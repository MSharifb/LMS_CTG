set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go



/*
	AUTHOR  : MD. OBAIDUL HAQUE SARKER
	DATE    : APR 24, 2008
	PURPOSE : TO FIND THE LEAVE RULE ID ACCORDING TO PRIORITY
	
	UPDATE BY : MD. SHAIFUL ISLAM
	DATE : OCT 25 2010

	PRIORITY:
		EMPLOYEE ID  =1
		GENDER		 =2
		CATEGORY	 =3
		DESIGNATION  =4
		DEPARTMENT   =5
		LOCATION	 =6
		COMPANY		 =7

		A=GENDER
		B=CATEGORY
		C=DESIGNATION
		D=DEPARTMENT
		E=LOCATION
*/



CREATE FUNCTION [dbo].[FN_GetAllAdjustWithLeaveRuleID]
(
	@strEmpID		VARCHAR(50),
	@intLeaveTypeID	INT,
	@strCompanyID   VARCHAR(50)
)
RETURNS	@tblRuleID TABLE
(
	RuleID		INT
)
AS
BEGIN
	
	DECLARE	@intRuleID				INT,
			@strDesignationID		VARCHAR(50),
			@strDepartmentID		VARCHAR(50),
			@strLocationID			VARCHAR(50),
			@intCategoryID			INT,
			@strGender				VARCHAR(10)
		
	SELECT   
			@strDesignationID=strDesignationID,
			@strDepartmentID=strDepartmentID,
			@strGender=strGender,
			@strLocationID=strLocationID,
			@intCategoryID=intCategoryCode
	FROM  LMS_tblEmployee
	WHERE strEmpID=@strEmpID AND strCompanyID=@strCompanyID
	
	SET @strDesignationID=ISNULL(@strDesignationID,'0')
	SET @strDepartmentID=ISNULL(@strDepartmentID,'0')
	SET @strLocationID=ISNULL(@strLocationID,'0')
	SET @strGender=ISNULL(@strGender,'0')
	SET @intCategoryID=ISNULL(@intCategoryID,0)
	SET @strCompanyID=ISNULL(@strCompanyID,'0')

	--BY EMPLOYEE CODE
	INSERT INTO @tblRuleID
	SELECT  intRuleID FROM
	(
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 1 AS intPriority
	FROM   LMS_tblLeaveRuleAssignment 
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE	intAdjustLeaveTypeID=@intLeaveTypeID AND
			ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND  
			strEmpID=@strEmpID
	)
	UNION ALL --ABCDE -5
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID, 2 AS intPriority
	FROM    LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND 
		  strGender=@strGender AND
		  intCategoryCode =@intCategoryID AND
		  strDesignationID =@strDesignationID AND  
		  strDepartmentID =@strDepartmentID AND 
		  strLocationID =@strLocationID AND
		  not strGender is null AND
		  ISNULL(intCategoryCode,0)>0 AND
		  not strDesignationID is null AND  
		  not strDepartmentID is null AND 
		  not strLocationID is null 
	)	
	UNION ALL ---ABCD -4
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 3 AS intPriority
	FROM    LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND 
		  strGender=@strGender AND
		  intCategoryCode =@intCategoryID AND  
		  strDesignationID =@strDesignationID AND  
		  strDepartmentID =@strDepartmentID AND
		  not strGender is null AND
		  ISNULL(intCategoryCode,0)>0 AND
		  not strDesignationID is null AND  
		  not strDepartmentID is null AND
		  strLocationID is null 
	)
	UNION ALL	--ABCE	--4
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 4 AS intPriority
	FROM    LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND 
		  strGender=@strGender AND
		  intCategoryCode =@intCategoryID AND   
		  strDesignationID =@strDesignationID AND
		  strLocationID =@strLocationID AND
		  not strGender is null AND
		  ISNULL(intCategoryCode,0)>0 AND
		  not strDesignationID is null AND 
		  not strLocationID is null AND
		  strDepartmentID is null 
 
	)
	UNION ALL	--ABDE --4		
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 5 AS intPriority
	FROM    LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND 
		  strGender=@strGender AND
		  intCategoryCode =@intCategoryID AND   
		  strDepartmentID =@strDepartmentID AND
		  strLocationID =@strLocationID AND
		  not strGender is null AND
		  ISNULL(intCategoryCode,0)>0 AND
		  not strDepartmentID is null AND 
		  not strLocationID is null AND
		  strDesignationID is null  
			
	)
	UNION ALL --ACDE  --4
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 6 AS intPriority
	FROM    LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND 
		  strGender=@strGender AND
		  strDesignationID =@strDesignationID AND  
		  strDepartmentID =@strDepartmentID AND 
		  strLocationID =@strLocationID AND
		  not strGender is null AND
		  not strDesignationID is null AND  
		  not strDepartmentID is null AND 
		  not strLocationID is null AND 
          intCategoryCode=0   
			
	)
	UNION ALL --BCDE --4
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 7 AS intPriority
	FROM    LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND
		  intCategoryCode =@intCategoryID AND
		  strDesignationID =@strDesignationID AND  
		  strDepartmentID =@strDepartmentID AND 
		  strLocationID =@strLocationID AND
		  ISNULL(intCategoryCode,0)>0 AND
		  not strDesignationID is null AND  
		  not strDepartmentID is null AND 
		  not strLocationID is null AND
		  strGender is null  
	)

	UNION ALL	--ABC	--3
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 8 AS intPriority
	FROM    LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND 
		  strGender=@strGender AND
		  intCategoryCode =@intCategoryID AND   
		  strDesignationID =@strDesignationID AND
		  not strGender is null AND
		  ISNULL(intCategoryCode,0)>0 AND
		  not strDesignationID is null AND  
		  strDepartmentID is null AND 
		  strLocationID is null 
	)
	UNION ALL	--ABD	--3
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 9 AS intPriority
	FROM    LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND 
		  strGender=@strGender AND
		  intCategoryCode =@intCategoryID AND   
		  strDepartmentID =@strDepartmentID AND 
		  not strGender is null AND
		  ISNULL(intCategoryCode,0)>0 AND
		  not strDepartmentID is null AND 
		  strDesignationID is null AND			
		  strLocationID is null 
	)

	UNION ALL	--ABE	--3
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 10 AS intPriority
	FROM    LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND 
		  strGender=@strGender AND
		  intCategoryCode =@intCategoryID AND  
		  strLocationID =@strLocationID AND
		  not strGender is null AND
		  ISNULL(intCategoryCode,0)>0 AND
		  not strLocationID is null AND
		  strDesignationID is null AND 
		  strDepartmentID is null 
  
	)

	UNION ALL	--ACD	--3
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 11 AS intPriority
	FROM    LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND 
		  strGender=@strGender AND
		  strDesignationID =@strDesignationID  AND   
		  strDepartmentID =@strDepartmentID AND
		  not strGender is null AND		  
		  not strDesignationID is null AND  
		  not strDepartmentID is null AND 
		  intCategoryCode=0 AND
		  strLocationID is null 
	)

	UNION ALL	--ACE	--3
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 12 AS intPriority
	FROM    LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND 
		  strGender=@strGender AND
		  strDesignationID =@strDesignationID  AND   
		  strLocationID =@strLocationID AND
		  not strGender is null AND
		  not strDesignationID is null AND 
		  not strLocationID is null AND 
		  intCategoryCode=0 AND
		  strDepartmentID is null 
			 
	)

	UNION ALL	--ADE	--3
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 13 AS intPriority
	FROM    LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND 
		  strGender=@strGender AND
		  strDepartmentID =@strDepartmentID  AND   
		  strLocationID =@strLocationID AND 
		  not strGender is null AND
		  not strDepartmentID is null AND 
		  not strLocationID is null AND
		  intCategoryCode=0 AND   
		  strDesignationID is null 
			
	)

	UNION ALL	--BCD	--3
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 14 AS intPriority
	FROM    LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND		  	  
		  intCategoryCode =@intCategoryID AND   
		  strDesignationID =@strDesignationID AND
		  strDepartmentID =@strDepartmentID AND		
		  ISNULL(intCategoryCode,0)>0 AND
		  not strDesignationID is null AND  
		  not strDepartmentID is null AND 
		  strGender is null AND			
		  strLocationID is null
	)

	UNION ALL	--BCE	--3
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 15 AS intPriority
	FROM    LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND
		  intCategoryCode =@intCategoryID AND   
		  strDesignationID =@strDesignationID AND
		  strLocationID =@strLocationID AND
		  ISNULL(intCategoryCode,0)>0 AND
		  not strDesignationID is null AND 
		  not strLocationID is null AND
		  strGender is null AND    		
		  strDepartmentID is null 
			
	)

	UNION ALL	--BDE	--3
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 16 AS intPriority
	FROM    LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND		    
		  intCategoryCode =@intCategoryID AND   
		  strDepartmentID =@strDepartmentID AND
		  strLocationID =@strLocationID AND
		  ISNULL(intCategoryCode,0)>0 AND
		  not strDepartmentID is null AND 
		  not strLocationID is null AND
		  strGender is null AND		
		  strDesignationID is null  
	)

	UNION ALL	--CDE	--3
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 17 AS intPriority
	FROM    LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND
		  strDesignationID =@strDesignationID AND   
		  strDepartmentID =@strDepartmentID AND
		  strLocationID =@strLocationID AND
		  not strDesignationID is null AND
		  not strDepartmentID is null AND 
		  not strLocationID is null AND
		  intCategoryCode=0 AND
		  strGender is null 
			
	)

	UNION ALL --AB	--2
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 18 AS intPriority
	FROM    LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID 
		  AND strApplyType='All' AND 
		  strGender=@strGender AND 
		  intCategoryCode =@intCategoryID AND
		  not strGender is null AND
		  ISNULL(intCategoryCode,0)>0 AND
		  strDesignationID is null AND 
		  strDepartmentID is null AND 
		  strLocationID is null 
	)
	UNION ALL --AC	--2
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 19 AS intPriority
	FROM    LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND
	      strGender=@strGender AND  
		  strDesignationID =@strDesignationID AND
		  not strGender is null AND
		  not strDesignationID is null AND
		  intCategoryCode=0 AND			
		  strDepartmentID is null AND 
		  strLocationID is null 
	)
	UNION ALL --AD	--2
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 20 AS intPriority
	FROM LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND 
		  strGender=@strGender AND 
		  strDepartmentID =@strDepartmentID AND
		  not strGender is null AND
		  not strDepartmentID is null AND
		  intCategoryCode=0 AND  
		  strDesignationID is null AND			
		  strLocationID is null  
	)

	UNION ALL --AE	--2
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 21 AS intPriority
	FROM LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND 
		  strGender=@strGender AND 
		  strLocationID =@strLocationID AND
		  not strGender is null AND
		  not strLocationID is null AND
		  intCategoryCode=0 AND  
		  strDesignationID is null AND 
		  strDepartmentID is null
	)

	UNION ALL --BC	--2
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 22 AS intPriority
	FROM LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		strApplyType='All' AND
	    intCategoryCode =@intCategoryID AND    
		strDesignationID =@strDesignationID AND
		ISNULL(intCategoryCode,0)>0 AND
		not strDesignationID is null AND
		strGender is null AND  
		strDepartmentID is null AND 
		strLocationID is null   
	)

	UNION ALL --BD	--2
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 23 AS intPriority
	FROM LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		strApplyType='All' AND
		intCategoryCode =@intCategoryID AND    
		strDepartmentID =@strDepartmentID AND
		ISNULL(intCategoryCode,0)>0 AND
		not strDepartmentID is null AND
		strGender is null AND   
		strDesignationID is null AND			
		strLocationID is null  
	)
	UNION ALL --BE	--2
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 24 AS intPriority
	FROM LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		strApplyType='All' AND
		intCategoryCode =@intCategoryID AND    
		strLocationID =@strLocationID AND
		ISNULL(intCategoryCode,0)>0 AND
		not strLocationID is null AND
		strGender is null AND        
		strDesignationID is null AND 
		strDepartmentID is null 
			 
	)

	UNION ALL --CD	--2
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 25 AS intPriority
	FROM LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		strApplyType='All' AND
		strDesignationID =@strDesignationID AND  
		strDepartmentID =@strDepartmentID AND
		not strDesignationID is null AND
		not strDepartmentID is null AND
		strGender is null AND 
		intCategoryCode=0 AND			
		strLocationID is null  
	)
	UNION ALL --CE	--2
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 26 AS intPriority
	FROM LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND
		  strDesignationID =@strDesignationID AND 
		  strLocationID =@strLocationID AND
		  not strDesignationID is null AND
		  not strLocationID is null AND
		  strGender is null AND 
		  intCategoryCode=0 AND			
		  strDepartmentID is null 
		    
	)
	UNION ALL --DE	--2
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 27 AS intPriority
	FROM LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND
		  strDepartmentID =@strDepartmentID AND 
		  strLocationID =@strLocationID AND
		  not strDepartmentID is null AND
		  not strLocationID is null AND
		  strGender is null AND
		  intCategoryCode=0 AND  
		  strDesignationID is null 
			
	)
	UNION ALL --A --1
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 28 AS intPriority
	FROM LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND 
		  strGender=@strGender AND
		  not strGender is null AND
		  intCategoryCode=0 AND  
		  strDesignationID is null AND 
		  strDepartmentID is null AND 
		  strLocationID is null  
	)
	UNION ALL --B --1
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 29 AS intPriority
	FROM LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND
		  intCategoryCode =@intCategoryID AND
		  ISNULL(intCategoryCode,0)>0 AND
		  strGender is null AND 
		  strDesignationID is null AND 
		  strDepartmentID is null AND 
		  strLocationID is null  
	)
	UNION ALL --C	--1
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 30 AS intPriority
	FROM LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND
		  strDesignationID =@strDesignationID AND
		  not strDesignationID is null AND
		  strGender is null AND 
		  intCategoryCode=0 AND		  
		  strDepartmentID is null AND 
		  strLocationID is null  
	)
	UNION ALL --D	--1
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 31 AS intPriority
	FROM LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		 ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		 strApplyType='All' AND
		 strDepartmentID =@strDepartmentID AND
		 not strDepartmentID is null AND
		 strGender is null AND  
		 intCategoryCode=0 AND  
		 strDesignationID is null AND 			
		 strLocationID is null  
	)
	UNION ALL --E	--1
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 32 AS intPriority
	FROM LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
		  ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
		  strApplyType='All' AND 
		  strLocationID =@strLocationID AND
		  not strLocationID is null AND
		  strGender is null AND
		  intCategoryCode=0 AND  
		  strDesignationID is null AND 
		  strDepartmentID is null
		    
	)
	UNION ALL --All
	(SELECT  LMS_tblLeaveRuleAssignment.intRuleID , 33 AS intPriority
	FROM LMS_tblLeaveRuleAssignment
	INNER JOIN LMS_tblLeaveRule ON LMS_tblLeaveRule.intRuleID=LMS_tblLeaveRuleAssignment.intRuleID
	WHERE intAdjustLeaveTypeID=@intLeaveTypeID AND 
			ISNULL(LMS_tblLeaveRuleAssignment.strCompanyID,'0')=@strCompanyID AND 
			strApplyType='All' AND 
			strGender is null AND
			intCategoryCode=0 AND  
			strDesignationID is null AND 
			strDepartmentID is null AND 
			strLocationID is null 
	)
)tblLeaveRule
ORDER BY intPriority	

	RETURN --ISNULL(@intRuleID,0)
END
