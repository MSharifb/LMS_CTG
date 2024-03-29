set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go







/*
	AUTHOR	: MD. OBAIDUL HAQUE SARKER
	DATE	: 24-APR-2010
	PURPOSE : EMPLOYEE'S YEARLY LEAVE ENTITLEMENT 
			  AND CARRY OVER TO THE NEXT YEAR

	declare @p5 int
set @p5=0
declare @p6 nvarchar(max)
set @p6=N'Successful'
exec LMS_uspLeaveEntitlement @intLeaveYearID=191,@strEmpID1=N'00258',@strCompanyID=N'1',@strIUser=N'islam',@numErrorCode=@p5 output,@strErrorMsg=@p6 output
select @p5, @p6

*/

ALTER PROCEDURE [dbo].[LMS_uspLeaveEntitlement]
		@intLeaveYearID		INT,
		@strEmpID1			VARCHAR(50),
		@strCompanyID		VARCHAR(50),
		@strIUser			VARCHAR(50),
		@numErrorCode		INT	OUTPUt,
		@strErrorMsg		VARCHAR(200) OUTPUT 
AS
	SET NOCOUNT ON

	/*
	--[For Test Purpose]
	DECLARE
		@intLeaveYearID		INT,
		@strEmpID1			VARCHAR(50),
		@strCompanyID		VARCHAR(50),
		@strIUser			VARCHAR(50)

		SET	@intLeaveYearID = 190
		SET	@strEmpID1 ='00262'
		SET	@strCompanyID ='1'
		SET	@strIUser ='admin'

		EXEC LMS_uspLeaveEntitlement 
			@intLeaveYearID,
			@strEmpID1,
			@strCompanyID,
			@strIUser,
			0,''
	*/
	
	DECLARE		  
			    @dtJoiningDate					DATETIME,
			    @dtConfirmationDate				DATETIME,			    
				@dtLeaveYearStartDate			DATETIME,
				@dtLeaveYearEndDate				DATETIME,
				@dtLeavePreYearStartDate		DATETIME,
				@dtLeavePreYearEndDate			DATETIME,
				@dtLeaveCalculationFrom			DATETIME,
				@dtEligibleFrom					DATETIME,
				@dtCalculationFrom				DATETIME,
				@dtLastObsoluteDate				DATETIME,				
				@dtInactiveDate					DATETIME,
				@intOpeningYearID				INT,
				@intPreLeaveYearID				INT,				
				@intLeaveYearDays				INT,
				@intLeaveTypeID					INT,	
				@intEarnLeaveUnitForDays		INT,
				@intRuleID						INT,
				@intMaxCarryForwardDays			INT,
				@intPreYearEligibleDays			INT,
				@intLeaveEligibleDays			INT,
				@intTotalPresentDays			INT,
				@intMaxYear						INT,
				@intEligibleAfterMonth			INT,
				@intCalculateAfterMonth			INT,
				@fltDuration					FLOAT,
				@fltEntitlement					FLOAT,
				@fltAvailed						FLOAT,
				@fltEncased						FLOAT,
				@fltOB							FLOAT,
				@fltCB							FLOAT,				
				@strEntitlementType				VARCHAR(50),
				@strEligibleAfter				VARCHAR(50),
				@strCalculationFrom				VARCHAR(50),				
				@strLeaveObsoluteMonth			VARCHAR(50),
				@strEmpID						VARCHAR(50),
				@strEarnLeaveCalculationType	VARCHAR(50),
				@bitIsCarryForward				BIT,
				@bitIsEarnLeave					BIT,
				@bitCarryOver					BIT,
				@bitIsEnjoyAtaTime				BIT,
				@bitIsEligible					BIT

	
	SET @numErrorCode = 0
	SET @strErrorMsg = 'Successful'

	IF @strEmpID1 IS NULL OR @strEmpID1='' SET @strEmpID1='0'


	--[check last yaer in ledger]
	SELECT @intMaxYear=max(intLeaveYearID) FROM dbo.LMS_tblLeaveLedger
	WHERE strCompanyID=@strCompanyID
	AND dbo.Fn_MakeEqualValue(strEmpID,@strEmpID1)=@strEmpID1

	--[set max value if null]
	SET @intMaxYear=ISNULL(@intMaxYear,0)

	

	--[get office hour]
	SELECT  @fltDuration=fltDuration FROM LMS_tblOfficeTime 	
	WHERE strCompanyID=@strCompanyID 
	AND intLeaveYearID=@intLeaveYearID
	--AND intLeaveYearID in (select intLeaveYearID  from LMS_tblOfficeTimeDetails)

	SET @fltDuration =ISNULL(@fltDuration,0)

	

	
	IF @intMaxYear>@intLeaveYearID
		---[check back year process]
		BEGIN
			RAISERROR ('Back year does not allow to process.', 16, 1)
			GOTO procError
		END	
	ELSE IF (@intMaxYear>0 and (@intMaxYear+1)<@intLeaveYearID) -- NOT EXIST IN LEAVE LEDGER
		---[check skip year to process]
		BEGIN
			RAISERROR ('Leave year does not allow to skip.', 16, 1)
			GOTO procError
		END
	ELSE IF (@fltDuration=0) 
		---[check year wise office hour]
		BEGIN
			RAISERROR ('Office hour have not set for the year.', 16, 1)
			GOTO procError
		END
	ELSE IF NOT EXISTS(SELECT  'EXITS' FROM LMS_tblOfficeTime 	
	WHERE strCompanyID=@strCompanyID 
	AND intLeaveYearID=@intLeaveYearID
	AND intLeaveYearID in (select intLeaveYearID  from LMS_tblOfficeTimeDetails))
		BEGIN
			RAISERROR ('Office hour details have not set for the year.', 16, 1)
			GOTO procError
		END

	---[get opening year id]
	SELECT Distinct @intOpeningYearID=min(intLeaveYearID)
	FROM LMS_tblLeaveOpening
	WHERE strCompanyID=@strCompanyID 

	SET @intOpeningYearID=Isnull(@intOpeningYearID,0)

	--[get leave year]
	SELECT @dtLeaveYearStartDate=dtStartDate,@dtLeaveYearEndDate=dtEndDate
	FROM LMS_tblLeaveYear
	WHERE intLeaveYearID=@intLeaveYearID 
	AND strCompanyID=@strCompanyID
  	
	--[get pre year start date and end date]
	SET @dtLeavePreYearStartDate=DATEADD(year,-1,@dtLeaveYearStartDate)
	SET @dtLeavePreYearEndDate=DATEADD(year,-1,@dtLeaveYearEndDate)
	
	---[get pre year id]
	SELECT @intPreLeaveYearID=intLeaveYearID
	FROM LMS_tblLeaveYear
	WHERE strCompanyID=@strCompanyID 
	AND dtStartDate=@dtLeavePreYearStartDate 
	AND dtEndDate=@dtLeavePreYearEndDate

	SET @intPreLeaveYearID=Isnull(@intPreLeaveYearID,0)

	--[leave year total days]
	SET @intLeaveYearDays=DATEDIFF(DAY,@dtLeaveYearStartDate,@dtLeaveYearEndDate)+1

	DECLARE CURLEAVE CURSOR 
	FOR
	SELECT intLeaveTypeID,bitIsEarnLeave,strEntitlementType,intEarnLeaveUnitForDays,strEarnLeaveCalculationType
	FROM LMS_tblLeaveType
	WHERE strCompanyID=@strCompanyID 

	 OPEN CURLEAVE
	 FETCH CURLEAVE INTO @intLeaveTypeID,@bitIsEarnLeave,@strEntitlementType,@intEarnLeaveUnitForDays,@strEarnLeaveCalculationType
	 WHILE @@FETCH_STATUS=0
	 BEGIN
			DECLARE CUREMP	CURSOR 
			FOR
			SELECT strEmpID,dtJoiningDate,dtConfirmationDate,dtInactiveDate
			FROM LMS_tblEmployee
			WHERE strCompanyID=@strCompanyID 
			AND dtJoiningDate<@dtLeaveYearEndDate 
			AND (dtInactiveDate IS NULL OR dtInactiveDate>@dtLeaveYearStartDate)
			AND dbo.Fn_MakeEqualValue(strEmpID,@strEmpID1)=@strEmpID1

			OPEN CUREMP
			FETCH	CUREMP	INTO @strEmpID,@dtJoiningDate,@dtConfirmationDate,@dtInactiveDate
			WHILE @@FETCH_STATUS=0
			BEGIN
				
				SET @intRuleID=0
				SET @bitIsEnjoyAtaTime=0

				--[check leave leadge entry allowed or not]
				IF NOT EXISTS(SELECT 'EXIST' FROM LMS_tblLeaveLedger WHERE strCompanyID=@strCompanyID AND strEmpID=@strEmpID AND intLeaveTypeID=@intLeaveTypeID AND intLeaveYearID=@intLeaveYearID AND bitIsManual=1)
				BEGIN 
					--[delete ledger entry]					
					DELETE FROM dbo.LMS_tblLeaveLedger 
					WHERE strEmpID=@strEmpID 
					AND intLeaveTypeID=@intLeaveTypeID 
					AND strCompanyID=@strCompanyID 
					AND intLeaveYearID=@intLeaveYearID
			
					--[get leave rule id]
					SET @intRuleID=dbo.FN_GetLeaveRuleID(@strEmpID,@intLeaveTypeID,@strCompanyID)

					IF @intRuleID<>0 
					BEGIN
							--[leave rule parameters]
							SELECT @fltEntitlement=fltEntitlement
									,@strEligibleAfter=strEligibleAfter
									,@strCalculationFrom=strCalculationFrom
									,@bitIsCarryForward=bitIsCarryForward
									,@intMaxCarryForwardDays=Isnull(intMaxCarryForwardDays,0)
									,@strLeaveObsoluteMonth=strLeaveObsoluteMonth
									,@intEligibleAfterMonth=ISNULL(intEligibleAfterMonth,0)
									,@intCalculateAfterMonth = intCalculateAfterMonth
							FROM LMS_tblLeaveRule
							WHERE strCompanyID=@strCompanyID AND intRuleID=@intRuleID
				
							--[find leave entitlement calculation form date]
								IF @strCalculationFrom='Joining Date'
									IF DATEADD(MONTH,@intCalculateAfterMonth,@dtJoiningDate)>@dtLeaveYearStartDate 
										SET @dtLeaveCalculationFrom=DATEADD(MONTH,@intCalculateAfterMonth,@dtJoiningDate)
									ELSE
										SET @dtLeaveCalculationFrom=@dtLeaveYearStartDate
								ELSE 
									BEGIN
										IF DATEADD(MONTH,@intCalculateAfterMonth,@dtConfirmationDate)>@dtLeaveYearStartDate 
											SET @dtLeaveCalculationFrom=DATEADD(MONTH,@intCalculateAfterMonth,@dtConfirmationDate)
										ELSE
											SET @dtLeaveCalculationFrom=@dtLeaveYearStartDate
									END
								

							---[find leave eligible date]
							IF @strEligibleAfter='Joining Date'
								BEGIN
									SET @dtEligibleFrom=@dtJoiningDate	
								END
							ELSE
								BEGIN
									SET @dtEligibleFrom=@dtConfirmationDate
								END

							
							

							SET @bitIsEligible=1
							SET @intEligibleAfterMonth=IsNull(@intEligibleAfterMonth,0)							
							IF @intEligibleAfterMonth >0 
								BEGIN
									IF dbo.FN_GetDateOnly(GETDATE()) < DATEADD(MONTH,@intEligibleAfterMonth,dbo.FN_GetDateOnly(@dtEligibleFrom)) 
										BEGIN
											SET @bitIsEligible=0											
										END
								END 
								
							---[leave eligible days]
							IF @bitIsCarryForward=1 AND @dtLeaveCalculationFrom>DATEADD(YEAR,-1,@dtLeaveYearStartDate) AND  @dtLeaveCalculationFrom<@dtLeaveYearStartDate
								AND NOT EXISTS(SELECT 'EXIST' FROM LMS_tblLeaveLedger WHERE strCompanyID=@strCompanyID AND strEmpID=@strEmpID AND intLeaveTypeID=@intLeaveTypeID AND intLeaveYearID=@intLeaveYearID)
								BEGIN
									SET @intPreYearEligibleDays=DATEDIFF(DAY,@dtLeaveCalculationFrom, DATEADD(DAY,-1,@dtLeaveYearStartDate))+1
									SET @dtLeaveCalculationFrom=@dtLeaveYearStartDate

									SELECT @intPreYearEligibleDays
								END
							ELSE
								BEGIN			
									SET @intPreYearEligibleDays=0 
								END

							IF @strCalculationFrom='Joining Date'
								BEGIN
									SET @dtCalculationFrom=@dtJoiningDate	
								END
							ELSE
								BEGIN
									SET @dtCalculationFrom=@dtConfirmationDate
								END

							IF ISNULL(@intCalculateAfterMonth,0) >0 
								BEGIN
									IF dbo.FN_GetDateOnly(GETDATE()) < DATEADD(MONTH,@intCalculateAfterMonth,dbo.FN_GetDateOnly(@dtCalculationFrom)) 
										BEGIN
											SET @intLeaveEligibleDays=0												
										END
									ELSE
										BEGIN
											SET @intLeaveEligibleDays=DATEDIFF(DAY,@dtLeaveCalculationFrom,@dtLeaveYearEndDate)+1											
										END
								END 
								ELSE
									BEGIN
										
											SET @intLeaveEligibleDays=DATEDIFF(DAY,@dtLeaveCalculationFrom,@dtLeaveYearEndDate)+1
											
											IF(@strCalculationFrom = 'N/A')
											BEGIN
													IF dbo.FN_GetDateOnly(GETDATE()) < dbo.FN_GetDateOnly(@dtEligibleFrom)
													BEGIN
														SET @intLeaveEligibleDays=0	
														SET @bitIsEligible = 0	
														PRINT '@dtEligibleFrom is '+ CAST( @dtEligibleFrom as VARCHAR)
													END

													IF(@intLeaveEligibleDays > 1)
													BEGIN
														SET @intLeaveEligibleDays= 365
													END
													ELSE
													BEGIN
														SET @intLeaveEligibleDays= 0 
													END
											END
										
									END
							--SET @intLeaveEligibleDays=DATEDIFF(DAY,@dtLeaveCalculationFrom,@dtLeaveYearEndDate)+1

--							IF(@intCalculateAfterMonth >0)
--							BEGIN
--								PRINT '@intLeaveEligibleDays IS '+ CAST(@intLeaveEligibleDays AS VARCHAR)+'  @intCalculateAfterMonth = '+CAST(@intCalculateAfterMonth AS VARCHAR)+' @dtLeaveCalculationFrom = '+CAST(@dtLeaveCalculationFrom AS VARCHAR)+' @dtLeaveYearEndDate = '+CAST(@dtLeaveYearEndDate AS VARCHAR)
--							END

							IF @intLeaveEligibleDays<0 SET	@intLeaveEligibleDays=0									
							
							--[leave ledger entry allowed after leave eligible date]			
							IF (@dtEligibleFrom<@dtLeaveYearEndDate AND @bitIsEligible=1)
							BEGIN
								
								--[check whether it is earn leave or not]
								IF @bitIsEarnLeave=1
									BEGIN
										IF @strEntitlementType='Calculated'
											BEGIN
												---[working days] 
												IF @strEarnLeaveCalculationType='Employee Attendance'
													BEGIN
														SELECT @intTotalPresentDays=SUM(intPresentDays)
														FROM LMS_tblEmpPresentDays WHERE strCompanyID=@strCompanyID AND strEmpID=@strEmpID AND intLeaveYearID=@intLeaveYearID
													END
												ELSE
													BEGIN

														DECLARE @dtLastDate AS DATETIME,
																@dtEarnLeaveCalculationFrom AS DATETIME
														
														SET @intTotalPresentDays=0
														IF @strCalculationFrom='N/A'
															SET @dtEarnLeaveCalculationFrom=@dtLeaveYearStartDate
														ELSE														
															IF @strCalculationFrom='Joining Date'
																IF @dtJoiningDate>@dtLeaveYearStartDate 
																	SET @dtEarnLeaveCalculationFrom=@dtJoiningDate
																ELSE
																	SET @dtEarnLeaveCalculationFrom=@dtLeaveYearStartDate
															ELSE
																IF @dtConfirmationDate>@dtLeaveYearStartDate 
																	SET @dtEarnLeaveCalculationFrom=@dtConfirmationDate
																ELSE
																	IF @dtConfirmationDate IS NOT NULL SET @dtEarnLeaveCalculationFrom=@dtLeaveYearStartDate

														IF GETDATE() between @dtLeaveYearStartDate and @dtLeaveYearEndDate
															SET @dtLastDate =GETDATE()
														ELSE
															SET @dtLastDate =@dtLeaveYearEndDate

														IF @dtInactiveDate IS NOT NULL AND @dtInactiveDate BETWEEN @dtLeaveYearStartDate AND @dtLeaveYearEndDate SET  @dtLastDate =@dtInactiveDate
														
														IF @dtEarnLeaveCalculationFrom IS NOT NULL  SET @intTotalPresentDays=DATEDIFF(DAY,@dtEarnLeaveCalculationFrom,@dtLastDate)+1
													END

												---[leave entitlement calculation from employee present days]												
												SET @fltEntitlement=ROUND((cast(@intTotalPresentDays as float)/cast(@intEarnLeaveUnitForDays as float)),2)*@fltDuration

											END
										ELSE
											BEGIN
												--[yearly entitlement]
												IF @strCalculationFrom='N/A'
													BEGIN
														SET @fltEntitlement=ISNULL(@fltEntitlement,0)
														SET @fltEntitlement=@fltEntitlement*@fltDuration
													END
												ELSE
													BEGIN
														SET @fltEntitlement=ISNULL(@fltEntitlement,0)
														SET @fltEntitlement=ROUND((@fltEntitlement/@intLeaveYearDays)*@intLeaveEligibleDays,0)*@fltDuration
													END
											END
									END		----------- end earn leave-----------------------------
								ELSE
									BEGIN
										--[yearly entitlement]
										IF @strCalculationFrom='N/A'
											BEGIN
												SET @fltEntitlement=ISNULL(@fltEntitlement,0)
												SET @fltEntitlement=@fltEntitlement*@fltDuration
											END
										ELSE
											BEGIN
												SET @fltEntitlement=ISNULL(@fltEntitlement,0)
												SET @fltEntitlement=ROUND((@fltEntitlement/@intLeaveYearDays)*@intLeaveEligibleDays,0)*@fltDuration
											END
									END


								--[calculate leave availed hours]
								SET @fltAvailed=dbo.[FN_GetLeaveTotalAvailed](
										@strEmpID,
										@intLeaveTypeID,
										@intLeaveYearID,
										@dtLeaveYearStartDate,
										@dtLeaveYearEndDate,
										@strCompanyID)

								---[calculate leave encasement hours]
								SELECT @fltEncased=SUM(fltEncaseDuration)
								FROM LMS_tblLeaveEncasment
								WHERE intLeaveTypeID=@intLeaveTypeID AND 
									  strEmpID=@strEmpID AND 
								      strCompanyID=@strCompanyID AND 
									  intLeaveYearID=@intLeaveYearID

								SET @fltEncased=ISNULL(@fltEncased,0)
								SET @fltEncased=ROUND((@fltEncased*@fltDuration),0)

								---[calculate opening balance]
								SET @fltOB=0
								IF @bitIsCarryForward=1
								BEGIN
									---[get previous year balance hours]
									SET @fltOB=dbo.[FN_GetLeaveOpeningBalance](
										@strEmpID,
										@intLeaveTypeID,
										@intPreLeaveYearID,
										@dtLeaveYearStartDate,
										@dtLeaveYearEndDate,
										@strCompanyID)

									-- Previouse year days which will be added to next year as a opening balance
									--IF @fltOB=0 AND @intPreYearEligibleDays>0 SET @fltOB=ROUND((@fltEntitlement/@intLeaveEligibleDays)*@intPreYearEligibleDays,0)

									--[check maximum carry over days]						
									DECLARE @fltMaxCarryOver AS FLOAT,
											@fltOBDay AS FLOAT

									IF @intMaxCarryForwardDays>0
										BEGIN											
											SET @fltMaxCarryOver=dbo.FN_ConvertDayToHour((case when isnull(@intPreLeaveYearID,0)=0 then @intLeaveYearID else @intPreLeaveYearID end),@strCompanyID,@intMaxCarryForwardDays)
											
											IF @fltMaxCarryOver > 0 and @fltMaxCarryOver<=@fltOB
											   BEGIN	 
													SET @fltOB=dbo.FN_ConvertDayToHour(@intLeaveYearID,@strCompanyID,@intMaxCarryForwardDays)
												END
											ELSE
												BEGIN
													SET @fltOBDay=dbo.FN_ConvertHourToDay((case when isnull(@intPreLeaveYearID,0)=0 then @intLeaveYearID else @intPreLeaveYearID end),@strCompanyID,@fltOB)
													SET @fltOB=dbo.FN_ConvertDayToHour(@intLeaveYearID,@strCompanyID,@fltOBDay)
												END												
										END
									ELSE
										BEGIN
											SET @fltOBDay=dbo.FN_ConvertHourToDay((case when isnull(@intPreLeaveYearID,0)=0 then @intLeaveYearID else @intPreLeaveYearID end),@strCompanyID,@fltOB)
											SET @fltOB=dbo.FN_ConvertDayToHour(@intLeaveYearID,@strCompanyID,@fltOBDay)
										END

									IF @strLeaveObsoluteMonth<>'N/A' AND @intOpeningYearID<>@intLeaveYearID
									BEGIN
										--[get last obsolute month]
										SET @dtLastObsoluteDate=dbo.FN_GetLastObsoluteDate(@strLeaveObsoluteMonth,@dtLeaveYearStartDate,@dtLeaveYearEndDate)
	
										--[opening balance will be obsolute after availed]
										IF GETDATE()>@dtLastObsoluteDate 
										BEGIN
											-- the following line is commented for the temporary solution
											-- we need to discuss with SA to resolve the issue on obsolute
											-- ****** ----
											--SET @fltOB=@fltOB-@fltAvailed-@fltEncased
											IF @fltOB<0 SET @fltOB=0
										END
												
									END
								END	--[end carry forward]
								
						SET @fltOB=isnull(@fltOB,0)
						SET @fltEntitlement=isnull(@fltEntitlement,0)
						SET @fltAvailed=isnull(@fltAvailed,0)
						SET @fltEncased=isnull(@fltEncased,0)

						---[calculate leave closing balance]
						SET @fltCB=@fltOB+@fltEntitlement-@fltAvailed-@fltEncased

						---[leave ledger]
						INSERT INTO LMS_tblLeaveLedger
						([intLeaveYearID]
						,[intLeaveTypeID]
						,[strEmpID]
						,[fltOB]
						,[fltEntitlement]
						,[fltAvailed]
						,[fltEncased]
						,[fltCB]
						,[bitIsManual]
						,[strCompanyID]
						,[strIUser]
						,[dtIDate]
						)
						VALUES
						(@intLeaveYearID
						,@intLeaveTypeID
						,@strEmpID
						,isnull(@fltOB,0)
						,isnull(@fltEntitlement,0)
						,isnull(@fltAvailed,0)
						,isnull(@fltEncased,0)
						,isnull(@fltCB,0)
						,0
						,@strCompanyID
						,@strIUser
						,getdate()
						)

						END	--[end leave eligible date for the employee]

						END --[leave rule id]
					END --[leave ledger entry allowed]

				FETCH	CUREMP	INTO @strEmpID,@dtJoiningDate,@dtConfirmationDate,@dtInactiveDate

			END  ---[end for employee cursor]
			CLOSE CUREMP
			DEALLOCATE CUREMP
			
			SET @strEmpID='0'

		FETCH CURLEAVE INTO @intLeaveTypeID,@bitIsEarnLeave,@strEntitlementType,@intEarnLeaveUnitForDays,@strEarnLeaveCalculationType
	END --[end for leave cursor] 
	CLOSE CURLEAVE
	DEALLOCATE CURLEAVE

	IF @@error <> 0 GOTO procError
	GOTO procEnd

procError:
	
	SET @numErrorCode = @@error
	SELECT @strErrorMsg = [description] 
	FROM master.dbo.sysmessages
 	WHERE error = @numErrorCode

	INSERT INTO error_log (LogDate,Source,ErrMsg)
	VALUES (getdate(),'LMS_uspLeaveEntitlement',@strErrorMsg)
procEnd:









