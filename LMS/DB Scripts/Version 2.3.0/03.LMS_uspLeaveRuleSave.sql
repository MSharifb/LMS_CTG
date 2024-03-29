set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


ALTER PROCEDURE [dbo].[LMS_uspLeaveRuleSave]
    @intRuleID						INT,
	@strRuleName					VARCHAR(50),
	@intLeaveTypeID					INT,
	@fltEntitlement					FLOAT,
	@strCalculationFrom				VARCHAR(50),
	@strEligibleAfter				VARCHAR(50),
	@bitIsEncashable				BIT,
	@intMaxEncahDays				INT,
	@intMinDaysInHand				INT,
	@bitIsCarryForward				BIT,
	@intMaxCarryForwardDays			INT,
	@strLeaveObsoluteMonth			VARCHAR(50),
	@bitIsIncludeHoliday			BIT,
	@bitIsIncludeWeekend			BIT,
	@bitIsIncludeWHForWOP			BIT,
	@intMaxLeaveDaysInApplication	INT,
	@intMaxLeaveAppInMonth			INT,
	@intMaxLeaveDaysInMonth			INT,
	@strCompanyID					VARCHAR(50),
	@intAdjustLeaveTypeID			INT,
	@bitIsEnjoyAtaTime				BIT,
	@strAllowType					VARCHAR(15),
	@intEligibleAfterMonth			INT,
	@intCalculateAfterMonth			INT,
	@strIUser						VARCHAR(50),
	@strEUser						VARCHAR(50),
    @strMode						VARCHAR(50),
	@numErrorCode					INT OUTPUT,
	@strErrorMsg					VARCHAR(200) OUTPUT
AS
	
	SET @numErrorCode = 0
	SET @strErrorMsg = 'Successful'
	SET @intAdjustLeaveTypeID=ISNULL(@intAdjustLeaveTypeID,0)

BEGIN TRY
 
	IF @strMode='I'
 
		BEGIN
		INSERT INTO LMS_tblLeaveRule
						(strRuleName,intLeaveTypeID,fltEntitlement,
						 strCalculationFrom,strEligibleAfter,intMaxEncahDays,
						 intMinDaysInHand,bitIsCarryForward,intMaxCarryForwardDays,
						 strLeaveObsoluteMonth,bitIsIncludeHoliday,bitIsIncludeWeekend,bitIsIncludeWHForWOP,
						 intMaxLeaveDaysInApplication,intMaxLeaveAppInMonth,intMaxLeaveDaysInMonth,
						 strCompanyID,strIUser,strEUser,dtIDate,dtEDate,intAdjustLeaveTypeID,
						 bitIsEnjoyAtaTime,strAllowType,intEligibleAfterMonth,intCalculateAfterMonth
 						)
					VALUES
						(@strRuleName,@intLeaveTypeID,@fltEntitlement,
						 @strCalculationFrom,@strEligibleAfter,@intMaxEncahDays,
						 @intMinDaysInHand,@bitIsCarryForward,@intMaxCarryForwardDays,
						 isnull(@strLeaveObsoluteMonth,''),@bitIsIncludeHoliday,@bitIsIncludeWeekend,@bitIsIncludeWHForWOP,
						 @intMaxLeaveDaysInApplication,@intMaxLeaveAppInMonth,@intMaxLeaveDaysInMonth,
						 @strCompanyID,@strIUser,@strEUser,getdate(),getdate(),@intAdjustLeaveTypeID,
						 @bitIsEnjoyAtaTime,isnull(@strAllowType,''),@intEligibleAfterMonth,@intCalculateAfterMonth
						)
		END
	ELSE
		BEGIN
			IF @strMode='U'
				BEGIN
					UPDATE LMS_tblLeaveRule SET
						strRuleName =@strRuleName,
						intLeaveTypeID=@intLeaveTypeID,
						fltEntitlement=@fltEntitlement,
						strCalculationFrom=@strCalculationFrom,
						strEligibleAfter=@strEligibleAfter,
						intMaxEncahDays=@intMaxEncahDays,
						intMinDaysInHand=@intMinDaysInHand,
						bitIsCarryForward=@bitIsCarryForward,
						intMaxCarryForwardDays=@intMaxCarryForwardDays,
						strLeaveObsoluteMonth=@strLeaveObsoluteMonth,
						bitIsIncludeHoliday=@bitIsIncludeHoliday,
						bitIsIncludeWeekend=@bitIsIncludeWeekend,
						bitIsIncludeWHForWOP = @bitIsIncludeWHForWOP,
						intMaxLeaveDaysInApplication=@intMaxLeaveDaysInApplication,
						intMaxLeaveAppInMonth=@intMaxLeaveAppInMonth,
						intMaxLeaveDaysInMonth=@intMaxLeaveDaysInMonth,	
						strEUser =@strEUser,dtEDate=getdate(),
						intAdjustLeaveTypeID =@intAdjustLeaveTypeID,
						bitIsEnjoyAtaTime =@bitIsEnjoyAtaTime,
						strAllowType =isnull(@strAllowType,''),
						intEligibleAfterMonth =@intEligibleAfterMonth,
						intCalculateAfterMonth = @intCalculateAfterMonth
					WHERE intRuleID=@intRuleID
				END

			IF @strMode='D'
				BEGIN
					DELETE FROM LMS_tblLeaveRule
					WHERE intRuleID=@intRuleID
				END
		END
END TRY

BEGIN CATCH
	SELECT @numErrorCode = ERROR_NUMBER()
	SELECT @strErrorMsg = [description] 
	FROM master.dbo.sysmessages
 	WHERE error = @numErrorCode

	INSERT INTO error_log (LogDate,Source,ErrMsg)
	VALUES (getdate(),'LMS_uspLeaveRuleSave',@strErrorMsg)
	
	DECLARE @strErrorCode AS VARCHAR(20);
	SET @strErrorCode = '-' + CAST(@numErrorCode AS VARCHAR(10))
	SET @numErrorCode = CAST(@strErrorCode AS INT)
END CATCH


	
	












