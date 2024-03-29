set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


/*
	AUTHOR   : MD. OBAIDUL HAQUE SARKER
	DATE	 : APR 29, 2010
	PURPOSE  : UPDATE LEDGER 
		AFTER 
		1. LEAVE APPROVED AND CANCELED
		2. LEAVE ENCASEMENT

	EDIT BY  : MD. SHAIFUL ISLAM
	DATE	 : FEB 14, 2011


*/


ALTER PROCEDURE [dbo].[LMS_uspLedgerUpdate]
	@intLeaveYearID		INT,
	@intLeaveTypeID		INT,
	@strEmpID			VARCHAR(50),
	@strCompanyID		VARCHAR(50)	
AS

	SET NOCOUNT ON
	
	DECLARE @intRuleID		INT,
			@fltAvailed		FLOAT,
			@fltAvailedOP	FLOAT,
			@fltAdjusted	FLOAT,
			@fltEncased		FLOAT,
			@fltDuration	FLOAT

	--[get office hour by leave year Id]
	SELECT  @fltDuration=fltDuration FROM LMS_tblOfficeTime 
	WHERE strCompanyID=@strCompanyID AND intLeaveYearID=@intLeaveYearID

	SET @fltDuration=ISNULL(@fltDuration,0)

	BEGIN
		--[get total availed from approved leave application]
		SELECT @fltAvailed=isnull(sum(fltWithPayDuration),0)
		FROM LMS_tblLeaveApplication
		WHERE strCompanyID=@strCompanyID 
			  AND intLeaveYearID=@intLeaveYearID
			  AND intLeaveTypeID=@intLeaveTypeID 
			  AND strEmpID=@strEmpID
			  AND intAppStatusID=1
			  AND bitIsAdjustment=0 

--		SELECT @fltAvailed=isnull(sum(fltAvailed),0)
--					FROM LMS_tblLeaveLedger
--					WHERE strCompanyID=@strCompanyID 
--						  AND intLeaveYearID=@intLeaveYearID
--						  AND intLeaveTypeID=@intLeaveTypeID 
--						  AND strEmpID=@strEmpID


		SET @fltAvailed=isnull(@fltAvailed,0)

		
		--[get total approved adjusted leave duration from application]
		SELECT @fltAdjusted=isnull(sum(fltWithPayDuration),0)
		FROM LMS_tblLeaveApplication
		WHERE strCompanyID=@strCompanyID 
			  AND intLeaveYearID=@intLeaveYearID
			  AND intLeaveTypeID=@intLeaveTypeID 
			  AND strEmpID=@strEmpID
			  AND intAppStatusID=1
			  AND bitIsAdjustment=1

		SET @fltAdjusted=isnull(@fltAdjusted,0)


		--[get total availed duration from opening balance]
		SELECT @fltAvailedOP=isnull(fltAvailed,0)
		FROM LMS_tblLeaveOpening
		WHERE strCompanyID=@strCompanyID 
			  AND intLeaveYearID=@intLeaveYearID
			  AND intLeaveTypeID=@intLeaveTypeID 
			  AND strEmpID=@strEmpID

		SET @fltAvailedOP=isnull(@fltAvailedOP,0)

		--[calculate total availed duration]
		SET @fltAvailed=isnull((@fltAvailed + @fltAvailedOP)-(@fltAdjusted),0)

		--[get total duration of encasement]
		SELECT @fltEncased=isnull(sum(fltEncaseDuration),0)
		FROM LMS_tblLeaveEncasment
		WHERE strCompanyID=@strCompanyID 
			  AND intLeaveYearID=@intLeaveYearID
			  AND intLeaveTypeID=@intLeaveTypeID 
			  AND strEmpID=@strEmpID
		
		SET @fltEncased=isnull(@fltEncased,0)*@fltDuration
		
		--[get employee's leave rule by leave type Id]
		SET @intRuleID=dbo.FN_GetLeaveRuleID(@strEmpID,@intLeaveTypeID,@strCompanyID)

		--[check no leave rule found]
		IF IsNull(@intRuleID,0) >0
		BEGIN
			DECLARE @intAdjustLeaveTypeId	 INT,
					@fltAvailedAdjustWith	 FLOAT,
					@fltAvailedAdjustWithOP  FLOAT,
					@fltAdjustedAdjustWith	 FLOAT,
					@fltEncasedAdjustWith	 FLOAT

			--[get adjust with leave type Id from assigned leave rule]
			SELECT @intAdjustLeaveTypeId=intAdjustLeaveTypeID			   
			FROM dbo.LMS_tblLeaveRule 
			WHERE intRuleID=@intRuleID
		
			IF IsNull(@intAdjustLeaveTypeId,0) >0
			   BEGIN
					---[update applied for leave type ledger]
					UPDATE LMS_tblLeaveLedger SET 
							fltAvailed= @fltAvailed
							,fltEncased=@fltEncased
							,fltCB=(fltOB+fltEntitlement)-(@fltAvailed+@fltEncased)
					FROM LMS_tblLeaveLedger
					WHERE strCompanyID=@strCompanyID AND 
						  intLeaveYearID=@intLeaveYearID AND 
						  intLeaveTypeID=@intLeaveTypeID AND 
						  strEmpID=@strEmpID

					--[get total availed from adjust with leave type application]

--					SELECT @fltAvailedAdjustWith=isnull(sum(fltWithPayDuration),0)
--					FROM LMS_tblLeaveApplication
--					WHERE strCompanyID=@strCompanyID 
--						  AND intLeaveYearID=@intLeaveYearID
--						  AND intLeaveTypeID=@intAdjustLeaveTypeId 
--						  AND strEmpID=@strEmpID
--						  AND intAppStatusID=1				 
--						  AND bitIsAdjustment=0 

					SELECT @fltAvailedAdjustWith=isnull(sum(fltAvailed),0)
					FROM LMS_tblLeaveLedger
					WHERE strCompanyID=@strCompanyID 
						  AND intLeaveYearID=@intLeaveYearID
						  AND intLeaveTypeID=@intAdjustLeaveTypeId 
						  AND strEmpID=@strEmpID
						 -- AND intAppStatusID=1				 
						--  AND bitIsAdjustment=0 

				
					SET @fltAvailedAdjustWith=isnull(@fltAvailedAdjustWith,0)
					SET @fltAvailed=isnull((@fltAvailedAdjustWith + @fltAvailed),0)

					--[get total availed from adjust with leave type opening]				
					SELECT @fltAvailedAdjustWithOP=isnull(fltAvailed,0) 
					FROM LMS_tblLeaveOpening
					WHERE strCompanyID=@strCompanyID 
						  AND intLeaveYearID=@intLeaveYearID
						  AND intLeaveTypeID=@intAdjustLeaveTypeId 
						  AND strEmpID=@strEmpID

					SET @fltAvailedAdjustWithOP=isnull(@fltAvailedAdjustWithOP,0)	
					SET @fltAvailed=isnull((@fltAvailedAdjustWithOP + @fltAvailed),0)

					--[get total adjusted from adjust with leave type application]
					SELECT @fltAdjustedAdjustWith=isnull(sum(fltWithPayDuration),0)
					FROM LMS_tblLeaveApplication
					WHERE strCompanyID=@strCompanyID 
						  AND intLeaveYearID=@intLeaveYearID
						  AND intLeaveTypeID=@intAdjustLeaveTypeId 
						  AND strEmpID=@strEmpID
						  AND intAppStatusID=1
						  AND bitIsAdjustment=1

					SET @fltAdjustedAdjustWith=isnull(@fltAdjustedAdjustWith,0)
					SET @fltAdjusted=isnull(@fltAdjustedAdjustWith,0)			

					--[calculate total availed duration]
					SET @fltAvailed=isnull((@fltAvailed - @fltAdjusted),0)

					--[get total encasement from adjust with leave type] 
					SELECT @fltEncasedAdjustWith=isnull(sum(fltEncaseDuration),0)
					FROM LMS_tblLeaveEncasment
					WHERE strCompanyID=@strCompanyID 
						  AND intLeaveYearID=@intLeaveYearID
						  AND intLeaveTypeID=@intAdjustLeaveTypeId 
						  AND strEmpID=@strEmpID

					SET @fltEncasedAdjustWith=isnull(@fltEncasedAdjustWith,0)*@fltDuration
					SET @fltEncased=isnull((@fltEncasedAdjustWith + @fltEncased),0)

					---[update adjust with leave type ledger]
					UPDATE LMS_tblLeaveLedger SET 
							fltAvailed= @fltAvailed
							,fltEncased=@fltEncased
							,fltCB=(fltOB+fltEntitlement)-(@fltAvailed+@fltEncased)
					FROM LMS_tblLeaveLedger
					WHERE strCompanyID=@strCompanyID 
						  AND intLeaveYearID=@intLeaveYearID 
						  AND intLeaveTypeID=@intAdjustLeaveTypeId 
						  AND strEmpID=@strEmpID
			   END
			ELSE
				BEGIN
					--[get leave rule as adjust with another leave type]
					-- Below line is commented by Rokan (10-07-2012)
					--SET @intRuleID=dbo.FN_GetAdjustWithLeaveRuleID(@strEmpID,@intLeaveTypeID,@strCompanyID)
				
					--[check no leave rule found]
					IF Isnull(@intRuleID,0) >0
					BEGIN
						DECLARE @intSubLeaveTypeId			 INT,
								@fltAvailedSubLeaveType		 FLOAT,
								@fltAvailedSubLeaveTypeOP	 FLOAT,
								@fltAdjustedSubLeaveType	 FLOAT,
								@fltEncasedSubLeaveType		 FLOAT

						--[get sub leave type Id by leave rule Id]
						SELECT @intSubLeaveTypeId=intLeaveTypeID			   
						FROM dbo.LMS_tblLeaveRule 
						WHERE intRuleID=@intRuleID

						IF Isnull(@intSubLeaveTypeId,0) >0
						   BEGIN

								--[get total availed from adjust with leave type application]
--								SELECT @fltAvailedSubLeaveType=isnull(sum(fltWithPayDuration),0)
--								FROM LMS_tblLeaveApplication
--								WHERE strCompanyID=@strCompanyID 
--									  AND intLeaveYearID=@intLeaveYearID
--									  AND intLeaveTypeID=@intSubLeaveTypeId 
--									  AND strEmpID=@strEmpID
--									  AND intAppStatusID=1	
--									  AND bitIsAdjustment=0

							SELECT @fltAvailedSubLeaveType=isnull(sum(fltAvailed),0)
							FROM LMS_tblLeaveLedger
							WHERE strCompanyID=@strCompanyID 
								  AND intLeaveYearID=@intLeaveYearID
								  AND intLeaveTypeID=@intSubLeaveTypeId 
								  AND strEmpID=@strEmpID

								SET @fltAvailedSubLeaveType=isnull(@fltAvailedSubLeaveType,0)
								SET @fltAvailed=isnull((@fltAvailedSubLeaveType + @fltAvailed),0)

								--[get total availed from adjust with leave type opening]							
								SELECT @fltAvailedSubLeaveTypeOP=isnull(fltAvailed,0)
								FROM LMS_tblLeaveOpening
								WHERE strCompanyID=@strCompanyID 
										  AND intLeaveYearID=@intLeaveYearID
										  AND intLeaveTypeID=@intSubLeaveTypeId 
										  AND strEmpID=@strEmpID

								SET @fltAvailedSubLeaveTypeOP=isnull(@fltAvailedSubLeaveTypeOP,0)
								SET @fltAvailed=isnull((@fltAvailed + @fltAvailedSubLeaveTypeOP),0)

								--[get total adjusted from adjust with leave type application]
								SELECT @fltAdjustedSubLeaveType=isnull(sum(fltWithPayDuration),0) 
								FROM LMS_tblLeaveApplication
								WHERE strCompanyID=@strCompanyID 
									  AND intLeaveYearID=@intLeaveYearID
									  AND intLeaveTypeID=@intSubLeaveTypeId 
									  AND strEmpID=@strEmpID
									  AND intAppStatusID=1
									  AND bitIsAdjustment=1

								SET @fltAdjustedSubLeaveType=isnull(@fltAdjustedSubLeaveType,0)
								SET @fltAdjusted=isnull(@fltAdjustedSubLeaveType,0)

								--[calculate total availed duration]
								SET @fltAvailed=isnull((@fltAvailed - @fltAdjusted),0)

								--[get total encasement from adjust with leave type] 
								SELECT @fltEncasedSubLeaveType=isnull(sum(fltEncaseDuration),0)
								FROM LMS_tblLeaveEncasment
								WHERE strCompanyID=@strCompanyID 
									  AND intLeaveYearID=@intLeaveYearID
									  AND intLeaveTypeID=@intSubLeaveTypeId 
									  AND strEmpID=@strEmpID
								
								SET @fltEncasedSubLeaveType=isnull(@fltEncasedSubLeaveType,0)*@fltDuration
								SET @fltEncased=isnull((@fltEncased + @fltEncasedSubLeaveType),0)

								--[update leave ledger]
								UPDATE LMS_tblLeaveLedger SET 
										fltAvailed= @fltAvailed
										,fltEncased=@fltEncased
										,fltCB=(fltOB+fltEntitlement)-(@fltAvailed+@fltEncased)
								FROM LMS_tblLeaveLedger
								WHERE strCompanyID=@strCompanyID 
									  AND intLeaveYearID=@intLeaveYearID 
									  AND intLeaveTypeID=@intLeaveTypeID 
									  AND strEmpID=@strEmpID
						   END
						 ELSE
							BEGIN
								--[update leave ledger]

								UPDATE LMS_tblLeaveLedger SET 
										fltAvailed=  @fltAvailed
										,fltEncased=@fltEncased
										,fltCB=(fltOB+fltEntitlement)-(@fltAvailed+@fltEncased)
								FROM LMS_tblLeaveLedger
								WHERE strCompanyID=@strCompanyID AND 
									  intLeaveYearID=@intLeaveYearID AND 
									  intLeaveTypeID=@intLeaveTypeID AND 
									  strEmpID=@strEmpID
							END 	
						END	
			   		ELSE
						BEGIN
							--[update applied for leave type ledger]

							UPDATE LMS_tblLeaveLedger SET 
									fltAvailed= @fltAvailed
									,fltEncased=@fltEncased
									,fltCB=(fltOB+fltEntitlement)-(@fltAvailed+@fltEncased)
							FROM LMS_tblLeaveLedger
							WHERE strCompanyID=@strCompanyID AND 
								  intLeaveYearID=@intLeaveYearID AND 
								  intLeaveTypeID=@intLeaveTypeID AND 
								  strEmpID=@strEmpID
						END 	
				END
		 END
	END	










