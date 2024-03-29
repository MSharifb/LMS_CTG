set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go



/*
	AUTHOR   : MD. OBAIDUL HAQUE SARKER
	DATE	 : APR 29, 2010
	PURPOSE  : TO GET LEAVE APPLICATION DURATION IN HOUR
			   1. EXCLUDE WEEKEND & HOLIDAY DURATION, IF APPLICABLE


*/

ALTER PROCEDURE [dbo].[LMS_uspGetLeaveAppDuration]
       @strEmpID				VARCHAR(50)
      ,@intLeaveYearID			INT
      ,@intLeaveTypeID			INT
	  ,@strApplicationType		VARCHAR(50)
      ,@dtApplyFromDate			DATETIME
      ,@dtApplyToDate			DATETIME
	  ,@strCompanyID			VARCHAR(50)
      ,@numErrorCode			INT OUTPUT
	  ,@strErrorMsg				VARCHAR(200) OUTPUT
AS
	
	SET NOCOUNT ON
	
	DECLARE @fltDuration			FLOAT,
			@intLeaveRuleID			INT,
			@intWeekendRuleID		INT,
			@bitIsIncludeHoliday	BIT,
			@bitIsIncludeWeekend	BIT,
			@fltWeekend				FLOAT,
			@fltHoliday				FLOAT,
			@fltCurrBalance			FLOAT,
			@fltOfficeTime			FLOAT

	SET @numErrorCode = 0
	SET @strErrorMsg = 'Successful'

	BEGIN
		IF @strApplicationType='FullDay'
			BEGIN	
				SET @fltDuration=DATEDIFF(DAY,@dtApplyFromDate,@dtApplyToDate)+1				
			END
		ELSE IF @strApplicationType='FullDayHalfDay'
			BEGIN
				SET @fltDuration=DATEDIFF(DAY,@dtApplyFromDate,@dtApplyToDate)+1-0.5
				
			END
		ELSE IF @strApplicationType='Hourly'
			BEGIN
				SET @fltDuration=1
			END

		-- [get leave balance]

		SET @fltCurrBalance = (SELECT ISNULL(T.fltCB,0)
					FROM LMS_tblLeaveLedger T
					INNER JOIN LMS_tblLeaveType LT ON LT.intLeaveTypeID=T.intLeaveTypeID
					WHERE T.strCompanyID = @strCompanyID  AND T.intLeaveYearID = @intLeaveYearID
					AND T.intLeaveTypeID = @intLeaveTypeID AND T.strEmpID =@strEmpID)

		SET @fltOfficeTime = (select fltDuration from LMS_tblOfficeTime OT inner join 
										LMS_tblLeaveYear LY on LY.intLeaveYearID = OT.intLeaveYearID
										where isnull(bitIsActiveYear,0) = 1 and LY.intLeaveYearID = @intLeaveYearID)

		SET @fltCurrBalance = ISNULL(@fltCurrBalance,0) / ISNULL(@fltOfficeTime,0)


		--[get leave rule ID]	
		SET @intLeaveRuleID=dbo.FN_GetLeaveRuleID(@strEmpID,@intLeaveTypeID,@strCompanyID)

		--[check whether weekend/holiday exclude or not]
		IF @intLeaveRuleID<>0 
			BEGIN
				SELECT @bitIsIncludeHoliday=bitIsIncludeHoliday,@bitIsIncludeWeekend=bitIsIncludeWeekend
				FROM LMS_tblLeaveRule WHERE intRuleID=@intLeaveRuleID		
			END 
		ELSE
			BEGIN
				SET @bitIsIncludeHoliday=0
				SET @bitIsIncludeWeekend=0
			END	

		--[get employee roaster or not]
		DECLARE @bitIsRoaster BIT
		SELECT @bitIsRoaster=bitIsRoaster
		FROM   LMS_tblEmployee WHERE strEmpID=@strEmpID AND strCompanyID=@strCompanyID	

	    IF @bitIsRoaster<>0 
			BEGIN
				SET @bitIsIncludeHoliday=0
				SET @bitIsIncludeWeekend=0
			END	

		--[get employee's weekedn rule ID]
		IF @bitIsIncludeHoliday=1 OR @bitIsIncludeWeekend=1 
		BEGIN
			SET @intWeekendRuleID=dbo.FN_GetHolidayRuleID(@strEmpID,@intLeaveYearID,@strCompanyID)
		END
		

		--[exclude holiday from duration]
		IF @intWeekendRuleID<>0 
		BEGIN
			SET @fltWeekend=0
			SET @fltHoliday=0

			WHILE @dtApplyFromDate<=@dtApplyToDate
			BEGIN

				
				IF EXISTS(SELECT 'EXIST' FROM  LMS_tblHolidayWeekDayRuleDetails INNER JOIN LMS_tblHolidayWeekDay 
						ON LMS_tblHolidayWeekDayRuleDetails.intHolidayWeekendID=LMS_tblHolidayWeekDay.intHolidayWeekendID
						WHERE intHolidayRuleID=@intWeekendRuleID AND strType='Weekend' AND dbo.FN_GetDateOnly(@dtApplyFromDate) BETWEEN dtDateFrom AND dtDateTo)						
						BEGIN
							
							 IF (@bitIsIncludeWeekend=1	)
							 BEGIN
								IF((@fltDuration - @fltWeekend - @fltHoliday)<= @fltCurrBalance)
								 BEGIN
										IF @strApplicationType='FullDayHalfDay' AND dbo.FN_GetDateOnly(@dtApplyFromDate)=dbo.FN_GetDateOnly(@dtApplyToDate)
										 
											BEGIN
											 
												SET @fltWeekend=@fltWeekend+0.5
											END
										ELSE
											BEGIN
												SET @fltWeekend=@fltWeekend+1
											END	
								END
							END
						END
				ELSE IF EXISTS(SELECT 'EXIST' FROM  LMS_tblHolidayWeekDayRuleDetails INNER JOIN LMS_tblHolidayWeekDay 
						ON LMS_tblHolidayWeekDayRuleDetails.intHolidayWeekendID=LMS_tblHolidayWeekDay.intHolidayWeekendID
						WHERE intHolidayRuleID=@intWeekendRuleID AND strType='Holiday' AND dbo.FN_GetDateOnly(@dtApplyFromDate) BETWEEN dtDateFrom AND dtDateTo)						
						BEGIN

							 IF (@bitIsIncludeHoliday=1	)
								 BEGIN
									IF((@fltDuration - @fltHoliday - @fltWeekend )<= @fltCurrBalance)
									 BEGIN

											IF @strApplicationType='FullDayHalfDay' AND dbo.FN_GetDateOnly(@dtApplyFromDate)=dbo.FN_GetDateOnly(@dtApplyToDate)
												BEGIN

													SET @fltHoliday=@fltHoliday+0.5
												END
											ELSE
												BEGIN
													SET @fltHoliday=@fltHoliday+1
												END
										END
								END
						END	
 
				SET @dtApplyFromDate=DATEADD(DAY,1,@dtApplyFromDate)
			END
		END
		

		--[exclude weekend from duration]
		IF  @bitIsIncludeWeekend=1
		BEGIN
			SET @fltDuration=Isnull(@fltDuration,0)-Isnull(@fltWeekend,0)
		END
		
		--[exclude Holiday from duration]
		IF  @bitIsIncludeHoliday=1
		BEGIN
			SET @fltDuration=Isnull(@fltDuration,0)-Isnull(@fltHoliday,0)
		END
		
		SELECT isnull(@fltDuration,0)

	END

	IF @@error <> 0 GOTO procError
	GOTO procEnd

procError:
	SET @numErrorCode = @@error
	SELECT @strErrorMsg = [description] 
	FROM master.dbo.sysmessages
	WHERE error = @numErrorCode

	INSERT INTO error_log (LogDate,Source,ErrMsg)
	VALUES (getdate(),'LMS_uspGetLeaveAppDuration',@strErrorMsg)
procEnd:






