set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


/*
	AUTHOR  : MD. OBAIDUL HAQUE SARKER
	DATE    : MAY 20,2010
	PURPOSE : TO GET TOTAL LEAVE AVAILED 
	EDIT BY	: MD. SHAIFUL ISLAM
	DATE	: FEB 14, 2011
*/

ALTER FUNCTION [dbo].[FN_GetLeaveTotalAvailed](
	@strEmpID			VARCHAR(50),
	@intLeaveTypeID		INT,
	@intLeaveYearID		INT,
	@dtLeaveYearStart	DATETIME,
	@dtLeaveYearEnd		DATETIME,
	@strCompanyID		INT
)
RETURNS	FLOAT
AS
BEGIN
	DECLARE @fltAvailed		FLOAT,
			@fltAdjusted	FLOAT,
			@fltOB			FLOAT,
			@intRuleID		INT

	-- [get total availed from leave application]
	SELECT @fltAvailed=Isnull(sum(fltWithPayDuration),0)
	FROM LMS_tblLeaveApplication
	WHERE intLeaveTypeID=@intLeaveTypeID 
			AND strEmpID=@strEmpID 
			AND strCompanyID=@strCompanyID
			AND intLeaveYearID=@intLeaveYearID 
			AND intAppStatusID=1
			AND bitIsAdjustment=0

	SET @fltAvailed=Isnull(@fltAvailed,0)

	-- [get total adjusted from application]
	SELECT @fltAdjusted=Isnull(sum(fltWithPayDuration),0)
	FROM LMS_tblLeaveApplication
	WHERE intLeaveTypeID=@intLeaveTypeID 
			AND strEmpID=@strEmpID 
			AND strCompanyID=@strCompanyID
			AND intLeaveYearID=@intLeaveYearID 
			AND intAppStatusID=1
			AND bitIsAdjustment=1

	SET @fltAdjusted=Isnull(@fltAdjusted,0)
	SET @fltAvailed=(Isnull(@fltAvailed,0)-Isnull(@fltAdjusted,0))
	
	-- [get total availed from opening]
	SELECT @fltOB=fltAvailed
	FROM LMS_tblLeaveOpening
	WHERE strCompanyID=@strCompanyID 
			AND strEmpID=@strEmpID 
			AND intLeaveTypeID=@intLeaveTypeID 
			AND dtBalanceDate BETWEEN @dtLeaveYearStart AND @dtLeaveYearEnd
	
	SET @fltOB=Isnull(@fltOB,0)

	--[get leave rule as adjust with another leave type]
	--SET @intRuleID=dbo.FN_GetAdjustWithLeaveRuleID(@strEmpID,@intLeaveTypeID,@strCompanyID)
	
	Declare crRule Cursor for select * from dbo.FN_GetAllAdjustWithLeaveRuleID(@strEmpID,@intLeaveTypeID,@strCompanyID)

	OPEN crRule

	Fetch next FROM crRule  into @intRuleID

	WHILE @@FETCH_STATUS = 0
	BEGIN

	--[check no leave rule found]
	IF Isnull(@intRuleID,0) >0
	BEGIN
		DECLARE @intSubLeaveTypeId			INT,
				@fltAvailedSubLeaveType		FLOAT,
				@fltAvailedSubLeaveTypeOP	FLOAT,
				@fltAdjustedSubLeaveType	FLOAT,
				@fltEncasedSubLeaveType		FLOAT

		-- [get parameters]
		SELECT @intSubLeaveTypeId=intLeaveTypeID			   
		FROM dbo.LMS_tblLeaveRule 
		WHERE intRuleID=@intRuleID

		IF Isnull(@intSubLeaveTypeId,0) >0
		   BEGIN
				-- [get total availed from leave application]
				SELECT @fltAvailedSubLeaveType=Isnull(sum(fltWithPayDuration),0)
				FROM LMS_tblLeaveApplication
				WHERE intLeaveTypeID=@intSubLeaveTypeId 
						AND strEmpID=@strEmpID 
						AND strCompanyID=@strCompanyID
						AND intLeaveYearID=@intLeaveYearID 
						AND intAppStatusID=1
						AND bitIsAdjustment=0
				
				SET @fltAvailedSubLeaveType=Isnull(@fltAvailedSubLeaveType,0)
				SET @fltAvailed=Isnull((@fltAvailedSubLeaveType + @fltAvailed),0)

				-- [get total adjusted from application]
				SELECT @fltAdjustedSubLeaveType=Isnull(sum(fltWithPayDuration),0)
				FROM LMS_tblLeaveApplication
				WHERE intLeaveTypeID=@intSubLeaveTypeId 
						AND strEmpID=@strEmpID 
						AND strCompanyID=@strCompanyID
						AND intLeaveYearID=@intLeaveYearID 
						AND intAppStatusID=1
						AND bitIsAdjustment=1

				SET @fltAdjustedSubLeaveType=Isnull(@fltAdjustedSubLeaveType,0)
				SET @fltAdjusted=Isnull(@fltAdjustedSubLeaveType,0)

				---[get total availed]
				SET @fltAvailed=(Isnull(@fltAvailed,0)-Isnull(@fltAdjusted,0))

				-- [get total availed from opening]				
				SELECT @fltAvailedSubLeaveTypeOP=fltAvailed 
				FROM LMS_tblLeaveOpening
				WHERE strCompanyID=@strCompanyID 
						AND strEmpID=@strEmpID 
						AND intLeaveTypeID=@intSubLeaveTypeId 
						AND dtBalanceDate BETWEEN @dtLeaveYearStart AND @dtLeaveYearEnd
			
				SET @fltAvailedSubLeaveTypeOP=Isnull(@fltAvailedSubLeaveTypeOP,0)			
				SET @fltOB=Isnull((@fltAvailedSubLeaveTypeOP + @fltOB),0)
		   END
	END
	Fetch next FROM crRule  into @intRuleID
	END

	CLOSE crRule
	DEALLOCATE crRule
	SET @fltAvailed=Isnull(@fltAvailed,0)+Isnull(@fltOB,0)
	RETURN @fltAvailed
END
