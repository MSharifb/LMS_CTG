/*
 * File:        alert.js
 * CVS:         $Id$
 * Description: Alert library
 * Author:      Allan Jardine
 * Created:     26-8-07
 * Modified:    $Date$ by $Author$
 * Language:    JavaScript
 * Project:     Alert
 * Version:     1.2
 * 
 * Copyright 2007 Allan Jardine. All rights reserved.
 * 
 * Public functions:
 *   fnAlert
 *   fnConfirm
 *   fnCustom
 * 
 * Private functions:
 *   _fnCreateAlert
 *   _fnButtonSelected
 *   _fnAnimateOut
 *   _fnAnimateOut
 *   _fnSetOpacity
 *   _fnGetOpacity
 *   _fnKeyPress
 *   _fnGetButtonIndexFromPosition
 *   _fnGetButtonPositionFromIndex
 *   _fnGetSelectedIndex
 *   _fnCreateMessage
 *   _fnCreateButtons
 *   _fnGetCellIndex
 *   _fnCalculateNumberCells
 *   _fnCalculateCellWidth
 *   _fnCreateButtonsTable
 *   _fnCreateDomStructure
 * 
 */

/*
 * Class:    Alert
 * Purpose:  Alert object initalisation
 * Returns:  -
 * Inputs:   -
 * Notes:
 *
 */
if (typeof Alert == "undefined") {
  var Alert = {};
}

/*
 * Property: _bAlerting
 * Purpose:  Indicators for if an alert is currently being shown or not
 */
Alert._bAlerting = false;


/*
 * Property: _iButtonChosen
 * Purpose:  Button which was clicked, either by a click or pressing enter
 */
Alert._iButtonChosen = null;


/*
 * Property: _aoQueue
 * Purpose:  Queue for calling alert multiple times
 */
Alert._aoQueue = new Array();


/*
 * Function: fnAlert
 * Purpose:  Typical alert box
 * Returns:  bool:true
 * Inputs:   string:sMessage - message to be displayed
 *           function:fnOkay (optional) - okay button function to run
 * Notes:    This function builds a typical 'okay' alert box using the Alert
 *   controls. It is expected that for consistency in UI if Alert is used at
 *   all then this function will be used rather than the global alert 
 *   function.
 *
 */
Alert.fnAlert = function( sMessage, fnOkay )
{
	// Set default function for okay button
	typeof fnOkay != 'undefined' ? null : fnOkay = null;
	
	return this._fnCreateAlert( {
		'sTitle': 'Alert',
		'sMessage': sMessage,
		'sDisplay': 'aaab',
		'aoButtons': [
			{
				'sLabel': 'Okay',
				'fnSelect': fnOkay,
				'sClass': 'selected',
				'cPosition': 'b'
			}
		]
	} );
};


/*
 * Function: fnConfirm
 * Purpose:  Typical confirm box
 * Returns:  bool:true (on okay) or bool:false (on cancel)
 * Inputs:   string:sMessage - message to be displayed
 *           function:fnOkay (optional) - okay button function to run
 *           function:fnCancel (optional) - cancel button function to run
 *   default:'Cancel'
 * Notes:    This function builds a typical 'okay/canccel' alert box using the
 *   Alert controls. It is expected that for consistency in UI if Alert is 
 *   used at all then this function will be used rather than the global  
 *   confirm function.
 *
 */
Alert.fnConfirm = function( sMessage, fnOkay, fnCancel )
{
	// Set default function for okay button
	typeof fnOkay != 'undefined' ? null : fnOkay = null;
	
	// Set default function for cancel button
	typeof fnCancel != 'undefined' ? null : fnCancel = null;
	
	// sReturn - used for the return value
	return this._fnCreateAlert( {
		'sTitle': 'Confirmation required',
		'sMessage': sMessage,
		'sDisplay': 'aacd',
		'aoButtons': [
			{
				'sLabel': 'Okay',
				'fnSelect': fnOkay,
				'sClass': 'selected',
				'cPosition': 'd'
			},
			{
				'sLabel': 'Cancel',
				'fnSelect': fnCancel,
				'cPosition': 'c'
			}
		]
	} );
};


/*
 * Function: fnCustom
 * Purpose:  Custom alert box - public wrapper for create alert function
 * Returns:  -
 * Inputs:   -
 *
 */
Alert.fnCustom = function( oParameters )
{
	this._fnCreateAlert( oParameters );
};


/*
 * Function: _fnCreateAlert
 * Purpose:  Create an 'Alert' dialogue box
 * Returns:
 *     bool:true
 * Inputs:   
 *   object:oParameters {
 *     string:sMessage - The HTML message to be displayed in the alert box
 *     string:sTitle (optional) - Display to be displayed - default:''
 *     string:sDisplay (optional) - Position defination for buttons
 *     string:sClass (optional) - Class for the whole damn box
 *     string:sBackgroundColor (optional) - background colour (outside box)
 *     string:fBackgroundOpacity (optional) - background opacity (outside box)
 *     int:iHeightPx (optional)  - height of the alert box, px
 *     int:iWidthPx (optional)  - width of the alert box, px
 *     funcation:fnPreDraw (optional)  - function to run just before display
 *     funcation:fnPreComplete (optional)  - function for before callback fns
 *     bool:bAnimate (optional)  - animate in and out the alert box
 *     array object:aoButtons - the buttons to be considered [ {
 *       string:sLabel - button label
 *       char:cPosition - position to draw the button based on sDisplay
 *       string:sClass - button CSS class
 *       int:iHeightPx - height of the button, px (takes priority over %)
 *       int:iWidthPx - width of the button, px (takes priority over %)
 *       int:iHeightPc - height of the button, %
 *       int:iWidthPc - width of the button, %
 *       function:fnSelect - Function to run if button is selected
 *       function:fnRollOver - Function to run if mouse is rolled over button
 *       function:fnRollOut - Function to run if mouse is rolled out of button
 *     }, ... ]
 *   }
 * Notes:
 *   I think it's only fair to try and explain on earth what I'm trying to do
 *   with the 'sDisplay' and the 'oButtons.cPosition' parameters. The way I'm
 *   thinking of it is rather like the CSS3 display property, but only in 1D.
 *   Best way is with a couple of ascii style examples...
 *   
 *   sDisplay: abcd
 *   +-------------+-------------+-------------+-------------+
 *   |  Button a   |  Button b   |  Button c   |  Button d   |
 *   +-------------+-------------+-------------+-------------+
 *   
 *   sDisplay: abbc
 *   +-------------+-------------+-------------+-------------+
 *   |  Button a   |          Button b         |  Button c   |
 *   +-------------+-------------+-------------+-------------+
 *   
 *   sDisplay: aab
 *   +-----------------+-------------------+-----------------+
 *   |             Button a                |    Button b     |
 *   +-----------------+-------------------+-----------------+
 *   
 */
Alert._fnCreateAlert = function( oParameters )
{
	// Check to see if an alert message is already being displayed
	// If there is one - then we queue the next alert message so it will be
	// displayed when the other one ends
	if ( this._bAlerting )
	{
		this._aoQueue[this._aoQueue.length++] = oParameters;
		return;
	}
	else
		this._bAlerting = true;
	
	// Store all the passed in parameters locally in the object so any function
	// can access them
	typeof oParameters.sMessage != 'undefined' ? 
		this._sMessage=oParameters.sMessage : this._sMessage='';
	
	typeof oParameters.sTitle != 'undefined' ? 
		this._sTitle=oParameters.sTitle : this._sTitle='';
		
	typeof oParameters.sDisplay != 'undefined' ? 
		this._sDisplay=oParameters.sDisplay : this._sDisplay=null;
		
	typeof oParameters.sClass != 'undefined' ? 
		this._sClass=oParameters.sClass : this._sClass=null;
		
	typeof oParameters.sBackgroundColor != 'undefined' ? 
		this._sBackgroundColor=oParameters.sBackgroundColor : 
		this._sBackgroundColor=null;
		
	typeof oParameters.fBackgroundOpacity != 'undefined' ? 
		this._fBackgroundOpacity=oParameters.fBackgroundOpacity : 
		this._fBackgroundOpacity=null;
		
	typeof oParameters.iHeightPx != 'undefined' ? 
		this._iHeightPx=oParameters.iHeightPx : this._iHeightPx=null;
		
	typeof oParameters.iWidthPx != 'undefined' ? 
		this._iWidthPx=oParameters.iWidthPx : this._iWidthPx=null;
		
	typeof oParameters.fnPreDraw != 'undefined' ? 
		this._fnPreDraw=oParameters.fnPreDraw : this._fnPreDraw=null;
		
	typeof oParameters.fnPreComplete != 'undefined' ? 
		this._fnPreComplete=oParameters.fnPreComplete : this._fnPreComplete=null;
	
	typeof oParameters.bAnimate != 'undefined' ? 
		this._bAnimate=oParameters.bAnimate : this._bAnimate=true;
		
	typeof oParameters.aoButtons != 'undefined' ? 
		this._aButtons=oParameters.aoButtons : this._aButtons=null;
	
	this._bWaitForSelection = true;
	this._End = 0;
	
	// Create the alert box
	this._fnCalculateNumberCells( );
	this._fnCreateDomStructure( );
	this._fnCreateMessage( );
	this._fnCreateButtonsTable( );
	this._fnCreateButtons( );
	
	// Custom pre display function
	if ( typeof this._fnPreDraw == 'function' )
	{
		this._fnPreDraw.call( this ); // this (Alert) scope
	}
	
	
	jsCore.addListener( { mElement: document,
		                       sType: 'keyup',
		                  fnCallback: this._fnKeyPress,
		                        oObj: this,
		                      mScope: true } );
	
	/* We need to take focus away from any links that might have been activated
	 * in order to start alert. This is sub-optimal, but not bad and totally 
	 * cross browser compatable.
	 */
	var nLinks = document.getElementsByTagName('a');
	for ( var i=0 ; i<nLinks.length ; i++ )
	{
		nLinks[i].blur();
	}
	
	// Set the animation up
	if ( this._bAnimate )
	{
		this._iAnimateFrames = 0;
		this._iAnimateOpapacityStart = 0;
		this._fBackgroundOpacity == null ? this._iAnimateOpapacityEnd=0.1 : 
			this._iAnimateOpapacityEnd=this._fBackgroundOpacity;
		
		this._fnSetOpacity( document.getElementById('alert_wrapper'), 
			this._iAnimateOpapacityStart );
			
		this._fnSetOpacity( document.getElementById('alert_background'), 
			this._iAnimateOpapacityStart );
		
		this._fnAnimateIn( );
	}
	
	// Show the alert box
	document.getElementById('alert_wrapper').style.display = 'block';
	document.getElementById('alert_background').style.display = 'block';
};


/*
 * Function: _fnButtonSelected
 * Purpose:  A button was selected
 * Returns:  -
 * Inputs:   event:e
 *
 */
Alert._fnButtonSelected = function( e )
{	
	// If there is a check function for when a button is selected then we run
	// it now
	if ( this._fnPreComplete != null )
	{
		var bCheck = this._fnPreComplete.call( this ); // global scope
		if ( bCheck == false )
		{
			return;
		}
	}
	
	// Find out which button was selected
	if ( this._iButtonChosen != null )
	{
		var iButton = this._iButtonChosen;
		var nButton = document.getElementById('alert_buttons').getElementsByTagName('a')[iButton];
	}
	else
	{
		var nButton = jsCore.getElementFromEvent( e );
		var iButton = nButton.getAttribute( 'button' );
	}
	
	if ( !iButton )
	{
		iButton = 0;
	}
	
	// Run it's clal back function if needed
	if ( this._aButtons[iButton].fnSelect != null )
	{
		this._aButtons[iButton].fnSelect.call( window ); // global scope
	}
	
	if ( this._bAnimate )
	{
		this._fnAnimateOut( );
	}
	else
	{
		this._fnCleanUp( );
	}
}


/*
 * Function: _fnCleanUp
 * Purpose:  Delete DOM objects and clean Alert namespace
 * Returns:  -
 * Inputs:   -
 *
 */
Alert._fnCleanUp = function( )
{
	// Clean up the DOM
	document.getElementById('alert_wrapper').parentNode.removeChild(
		document.getElementById('alert_wrapper') );
	
	document.getElementById('alert_background').parentNode.removeChild(
		document.getElementById('alert_background') );
	
	// Remove listeners
	jsCore.removeListener( { mElement: document,
		                          sType: 'keyup',
		                     fnCallback: this._fnKeyPress } );
	
	// All done - reset internal properties to null
	this._sMessage = null;
	this._sTitle = null;
	this._sDisplay = null;
	this._sBackgroundColor = null;
	this._fBackgroundOpacity = null;
	this._iHeightPx = null;
	this._iWidthPx = null;
	this._fnPreDraw = null;
	this._aButtons = null;
	this._iButtonChosen = null;
	
	this._bAlerting = false;
	
	if ( this._aoQueue.length != 0 )
	{
		this._fnCreateAlert( this._aoQueue[0] );
		
		// Remove the item from the array
		this._aoQueue.splice( 0, 1 );
	}
};



/*
 * Support functions
 */


/*
 * Function: _fnAnimateIn
 * Purpose:  Animate in an alert box
 * Returns:  -
 * Inputs:   -
 *
 */
Alert._fnAnimateIn = function( )
{
	var bRunAgain = false;
	var nWrapper = document.getElementById('alert_wrapper');
	var nBackground = document.getElementById('alert_background');
	
	// Background opacity
	// NOTE: This acutally just sets the background opacity rather than
	// animating it in. Reason for this is most browsers are too slow
	if ( this._fnGetOpacity( nBackground ) < this._iAnimateOpapacityEnd )
	{
		this._fnSetOpacity( nBackground, this._iAnimateFrames*
			(this._iAnimateOpapacityEnd - this._iAnimateOpapacityStart) );
		bRunAgain = true;
	}
	
	// Alert box opacity
	if ( this._fnGetOpacity( nWrapper ) < 1 )
	{
		this._fnSetOpacity( nWrapper, this._iAnimateFrames*
			(1 - this._iAnimateOpapacityStart)/10 );
		bRunAgain = true;
	}
	
	this._iAnimateFrames++;
	
	if ( bRunAgain )
	{
		setTimeout( function() { Alert._fnAnimateIn(); }, 100 );
	}
}


/*
 * Function: _fnAnimateOut
 * Purpose:  Animate out an alert box
 * Returns:  -
 * Inputs:   -
 *
 */
Alert._fnAnimateOut = function( )
{
	var bRunAgain = false;
	var nWrapper = document.getElementById('alert_wrapper');
	var nBackground = document.getElementById('alert_background');
	
	// Background opacity
	// NOTE: This acutally just sets teh background opacity rather than
	// animating it in. Reason for this is most browsers are too slow
	if ( this._iAnimateFrames <= 0  )
	{
		this._fnSetOpacity( nBackground, 0 );
	}
	else
	{
		bRunAgain = true;
	}
	
	// Alert box opacity
	if ( this._fnGetOpacity( nWrapper ) > 0 )
	{
		this._fnSetOpacity( nWrapper, this._iAnimateFrames*
			(1 - this._iAnimateOpapacityStart)/10 );
		bRunAgain = true;
	}
	
	// Animate out quicker than we animated in
	this._iAnimateFrames -= 3;
	
	if ( bRunAgain )
	{
		setTimeout( function() { Alert._fnAnimateOut(); }, 100 );
	}
	else
	{
		this._fnCleanUp( );
	}
}


/*
 * Function: _fnSetOpacity
 * Purpose:  Set the opacity of a node in a cross browser manner
 * Returns:  -
 * Inputs:   
 *   node:nNode - the node we want to set the opacity of
 *   float:fOpacity - the opacity to set
 *
 */
Alert._fnSetOpacity = function( nNode, fOpacity )
{
	if ( !nNode )
		return;
	if ( typeof nNode.style.opacity != 'undefined' )
		nNode.style.opacity = fOpacity;
	else if ( typeof nNode.style.MozOpacity != 'undefined' )
		nNode.style.MozOpacity = fOpacity;
	else if ( typeof nNode.style.filter != 'undefined' )
	{
		nNode.style.filter = 'alpha(opacity=' + parseInt(fOpacity*100) + ')';
		nNode.style.zoom=1;
	}
}


/*
 * Function: _fnGetOpacity
 * Purpose:  Set the opacity of a node in a cross browser manner
 * Returns:  -
 * Inputs:   node:nNode - the node we want the opacity of
 *
 */
Alert._fnGetOpacity = function( nNode )
{
	if ( !nNode )
		return;
	if ( typeof nNode.style.opacity != 'undefined' )
		return nNode.style.opacity;
	else if ( typeof nNode.style.MozOpacity != 'undefined' )
		return nNode.style.MozOpacity;
	else if ( typeof nNode.style.filter != 'undefined' )
	{
		return (nNode.filters('alpha').opacity / 100);
	}
}


/*
 * Function: _fnKeyPress
 * Purpose:  Deal with any key presses when the alert diagloug is displayed.
 *   This includes - return (submit), left and right arrows (move selected)
 * Returns:  -
 * Inputs:   e:event
 *
 */
Alert._fnKeyPress = function( e )
{
	var kCode = e.keyCode || e.which; // gets the keycode in ie or ns
	
	// Return key - confirm alert box
	if ( kCode == 13 )
	{
		// This might not be enough for damn firefox
		if ( e.preventDefault )
			e.preventDefault();
		else
			e.returnValue = false;
			
		if ( e.stopPropagation )
			e.stopPropagation();
		else
			e.cancelBubble = true;
		
		for ( var i=0 ; i<this._aButtons.length ; i++ )
		{
			if ( this._aButtons[i].sClass == 'selected' )
			{
				this._iButtonChosen = i;
				this._fnButtonSelected( e );
				return false;
			}
		}
		
		// Nothing selected
		return false;
	}
	
	var iCurrentlyHighlighted;
	var iToBeHighlighted;
	var iButtonIndex;
	var nButtons = document.getElementById('alert_buttons').
		getElementsByTagName('a');
	
	// Left arrow - select new button
	if ( kCode == 37 )
	{
		// Calculate the required positioning and indexes
		iButtonIndex = this._fnGetSelectedIndex( );
		iButtonNumber = this._fnGetButtonPositionFromIndex( iButtonIndex );
		iNewSelectedIndex = this._fnGetButtonIndexFromPosition( iButtonNumber-1 );
		
		// Check that the new button really is a button
		if ( iButtonNumber <= 0 )
		{
			return false;
		}
		
		// Set the Alert class button classes to be the new values used in the DOM
		this._aButtons[iButtonIndex].sClass = '';
		this._aButtons[iNewSelectedIndex].sClass = 'selected';
		
		// Set the values in the DOM for the classes
		nButtons[iButtonNumber].className = '';
		nButtons[iButtonNumber].parentNode.className = '';
		
		nButtons[iButtonNumber-1].className = 'selected';
		nButtons[iButtonNumber-1].parentNode.className = 'selected';
		
		return false;
	}
	
	// Right arrow
	if ( kCode == 39 )
	{
		// Calculate the required positioning and indexes
		iButtonIndex = this._fnGetSelectedIndex( );
		iButtonNumber = this._fnGetButtonPositionFromIndex( iButtonIndex );
		iNewSelectedIndex = this._fnGetButtonIndexFromPosition( iButtonNumber+1 );
		
		// Check that the new button really is a button
		if ( iButtonNumber >= this._aButtons.length || iNewSelectedIndex < 0 )
		{
			return false;
		}
		
		// Set the Alert class button classes to be the new values used in the DOM
		this._aButtons[iButtonIndex].sClass = '';
		this._aButtons[iNewSelectedIndex].sClass = 'selected';
		
		// Set the values in the DOM for the classes
		nButtons[iButtonNumber].className = '';
		nButtons[iButtonNumber].parentNode.className = '';
		
		nButtons[iButtonNumber+1].className = 'selected';
		nButtons[iButtonNumber+1].parentNode.className = 'selected';
		
		return false;
	}
	return false;
};


/*
 * Function: _fnGetButtonIndexFromPosition
 * Purpose:  Get the _aButtons index from the button position (as displayed
 *   on screen)
 * Returns:  int: - index of the button in the _aButtons array
 * Inputs:   int:iButtonNumber - The button number that we want the index of
 *
 */
Alert._fnGetButtonIndexFromPosition = function( iButtonNumber )
{
	/* Use the button position in the array */
	if ( typeof this._sDisplay == 'undefined' )
	{
		return iButtonNumber;
	}
	
	/* Use the display property */
	var acDisplay = this._sDisplay.split( '' );
	var iButton = 0;
	
	// Find out what the position property of the target button is
	for ( var i=0 ; i<this._aButtons.length ; i++ )
	{
		if ( this._fnGetButtonPositionFromIndex(i) == iButtonNumber )
		{
			return i;
		}
	}
	
	return -2;
};


/*
 * Function: _fnGetButtonPositionFromIndex
 * Purpose:  Get the button number (as displayed on screen) from the _aButtons
 *   index
 * Returns:  int: - button position on screen
 * Inputs:   int:iButtonIndex - index of the button we want the position of
 *
 */
Alert._fnGetButtonPositionFromIndex = function( iButtonIndex )
{
	/* Use the button position in the array */
	if ( typeof this._sDisplay == 'undefined' )
	{
		return iButtonIndex;
	}
	
	/* Use the display property */
	var acDisplay = this._sDisplay.split( '' );
	var iButton = 0;
	
	// Count the cells which do not match their following identifier
	for ( var i=0 ; i<acDisplay.length-1 ; i++ )
	{
		// If we have the button that we are looking for
		if ( this._aButtons[iButtonIndex].cPosition == acDisplay[i] )
		{
			return iButton;
		}
		
		if ( acDisplay[i] != acDisplay[i+1] )
		{
			// Only count cells which are actually used as buttons
			for ( var j=0 ; j<this._aButtons.length ; j++ )
			{
				if ( this._aButtons[j].cPosition == acDisplay[i] )
				{
					iButton++;
					continue;
				}
			}
		}
	}
	
	return iButton;
};


/*
 * Function: _fnGetSelectedIndex
 * Purpose:  Get the _aButtons index of the currently selected button
 * Returns:  -
 * Inputs:   -
 *
 */
Alert._fnGetSelectedIndex = function( )
{
	for ( var i=0 ; i<this._aButtons.length ; i++ )
	{
		if ( this._aButtons[i].sClass == 'selected' )
		{
			return i;
		}
	}
	
	return -1;
};


/*
 * Function: _fnCreateMessage
 * Purpose:  Create the header and message text inside the alert box
 * Returns:  -
 * Inputs:   -
 *
 */
Alert._fnCreateMessage = function( )
{
	if ( typeof this._sTitle != 'undefined' )
	{
		document.getElementById('alert_header').innerHTML = this._sTitle;
	}
	
	if ( typeof this._sMessage != 'undefined' )
	{
		document.getElementById('alert_content').innerHTML = this._sMessage;
	}
	
	/* 
	 * IE displays 'select' element on top of Alert - we block them out with an iframe - thanks
	 * to Nick Karasek for this fix
	 */
	if ( /msie/i.test(navigator.userAgent) && !/opera/i.test(navigator.userAgent) ) {
		document.getElementById('alert_background').innerHTML = "<iframe></iframe>";
	}
};


/*
 * Function: _fnCreateButtons
 * Purpose:  Create the buttons for the alert box
 * Returns:  -
 * Inputs:   -
 *
 */
Alert._fnCreateButtons = function( )
{
	var iPosition;
	var nTableCells = document.getElementById('alert_button_row').
		getElementsByTagName('td');
	var nButton;
	var nText;
	var sStyle;
	
	for ( var i=0 ; i<this._aButtons.length ; i++ )
	{
		sStyle = '';
		iPosition = this._fnGetCellIndex( i );
		
		nTableCells[iPosition].className = '';
		
		// Add the A tag for the button
		nButton = document.createElement( "a" );
		
		if ( this._aButtons[i].sClass )
		{
			nButton.className = this._aButtons[i].sClass;
			nTableCells[iPosition].className = this._aButtons[i].sClass;
		}
		
		if ( this._aButtons[i].iHeightPx )
			nButton.style.height = this._aButtons[i].iHeightPx+'px';
		else if ( this._aButtons[i].iHeightPc )
			nButton.style.height = this._aButtons[i].iHeightPc+'%';
		
		if ( this._aButtons[i].iWidthPx )
			nButton.style.width = this._aButtons[i].iWidthPx+'px';
		else if ( this._aButtons[i].iWidthPc )
			nButton.style.width = this._aButtons[i].iWidthPc+'%';
		
		nButton.setAttribute( 'button', i ); // Used for identifying onclick
		nTableCells[iPosition].appendChild( nButton );
		
		// Add the text to the button for the label
		nText = document.createTextNode( this._aButtons[i].sLabel );
		nButton.appendChild( nText );
		
		// Add events for clicking / rolling over and rolling out of the buttons
		if ( typeof this._aButtons[i].fnRollOver != 'undefined' )
		{
			jsCore.addListener( { mElement: nButton,
				                       sType: 'mouseover',
				                  fnCallback: this._aButtons[i].fnRollOver,
				                        oObj: this,
				                      mScope: true } );
		}
		
		if ( typeof this._aButtons[i].fnRollOut != 'undefined' )
		{
			jsCore.addListener( { mElement: nButton,
				                       sType: 'mouseout',
				                  fnCallback: this._aButtons[i].fnRollOut,
				                        oObj: this,
				                      mScope: true } );
		}
		
		jsCore.addListener( { mElement: nButton,
			                       sType: 'click',
			                  fnCallback: this._fnButtonSelected,
			                        oObj: this,
			                      mScope: true } );
	}
};


/*
 * Function: _fnGetCellIndex
 * Purpose:  Get the index of a cell for a button
 * Returns:  int: index of cell with the target button
 * Inputs:   int:iButton - button we want the index of
 *
 */
Alert._fnGetCellIndex = function( iButton )
{
	// Just use regular position in declaration
	if ( typeof this._aButtons[iButton].cPosition == 'undefined' )
	{
		return iButton;
	}
	// User position given by the display property
	else
	{
		var acDisplay = this._sDisplay.split( '' );
		var iCell = 0;
		
		// Count through the display property to find the correct position
		for ( var i=0 ; i<acDisplay.length-1 ; i++ )
		{
			if ( this._aButtons[iButton].cPosition == acDisplay[i] )
			{
				return iCell;
			}
			
			if ( acDisplay[i] != acDisplay[i+1] )
			{
				iCell++;
			}
		}
		
		// Must be the last cell
		return iCell;
	}
};


/*
 * Function: _fnCalculateNumberCells
 * Purpose:  Calculate from display how many cell to draw
 * Returns:  int - number of cells
 * Inputs:   -
 *
 */
Alert._fnCalculateNumberCells = function( )
{
	/* Use the number of buttons as the guide */
	if ( this._sDisplay == null || this._sDisplay == '' )
	{
		this._iNumberOfCells = this._aButtons.length;
	}
	
	/* Use the display property */
	var acDisplay = this._sDisplay.split( '' );
	var iCells = 0;
	
	// Count the cells which do not match their following identifier
	for ( var i=0 ; i<acDisplay.length-1 ; i++ )
	{
		if ( acDisplay[i] != acDisplay[i+1] )
		{
			iCells++;
		}
	}
	
	// There will always be at least one cell
	iCells++;
	
	this._iNumberOfCells = iCells;
};


/*
 * Function: _fnCalculateCellWidth
 * Purpose:  Compute the width a cell should be based on its positioning
 * Returns:  int: - Cell width calculated as a % of the total table width
 * Inputs:   int:iTargetButton - the button of interest
 *
 */
Alert._fnCalculateCellWidth = function( iTargetButton )
{
	/* Use the number of buttons as the guide */
	if ( this._sDisplay == null || this._sDisplay == '' )
	{
		return parseInt( 100 / this._iNumberOfCells );
	}
	
	/* Use the display property as the guide */
	var acDisplay = this._sDisplay.split( '' );
	var iSpanCount = 1; // Count the number of boxes this one spans
	var iCell = 0;
	
	for ( var i=0 ; i<acDisplay.length-1 ; i++ )
	{
		if ( acDisplay[i] != acDisplay[i+1] )
		{
			// If we have the button that we are interested in
			if ( iTargetButton == iCell )
			{
				return parseInt( (100*iSpanCount) / acDisplay.length );
			}
			else
			{
				iSpanCount = 0;
				iCell++;
			}
		}
		
		iSpanCount++;
	}
	
	// There will always be one cell at the end
	return parseInt( (100*iSpanCount) / acDisplay.length );
};

/*
 * Function: _fnCreateButtonsTable
 * Purpose:  Draw the buttons onto the alert box
 * Returns:  -
 * Inputs:   -
 * Notes:    I know that rendering the table using an 'html string' rather 
 *   than tusing DOM functions is a bit crap - but this is the only way that
 *   with work with IE.
 */
Alert._fnCreateButtonsTable = function( )
{
	var nButtons = document.getElementById('alert_buttons');
	var nButtonCell;
	var iCellWidth;
	var sHtml = '';
	
	// Create the table for button display
	sHtml += '<table id="alert_button_table" width="100%" height="100%" cellspacing="5">';
	sHtml += 	'<tr id="alert_button_row">';
	
	for ( var i=0 ; i<this._iNumberOfCells ; i++ )
	{
		// Set the width of the cells - note that this is not the actual button
		// width as can be passed in. That is set later
		iCellWidth = this._fnCalculateCellWidth( i );
		sHtml += '<td id="alert_button_cell_'+i+'" class="empty" width="'+iCellWidth+'%"></td>';
	}
	
	sHtml += 	'</tr>';
	sHtml += '</table>';
	
	nButtons.innerHTML = sHtml;
};


/*
 * Function: _fnCreateDomStructure
 * Purpose:  Add the required DOM elements on to the page for an 'Alert' box
 * Returns:  -
 * Inputs:   -
 *   
 */
Alert._fnCreateDomStructure = function( )
{
	// Create the Alert diagloue box div
	var nDialogue = document.createElement( "div" );
	nDialogue.setAttribute('id', 'alert_wrapper');
	nDialogue.style.display = 'none'; // Hide until populated
	
	if ( this._sClass != '' )
	{
		nDialogue.className = this._sClass;
	}
	
	document.getElementsByTagName('body')[0].insertBefore
		( nDialogue, document.body.childNodes[0] );
	
	if ( this._iHeightPx != null )
	{
		nDialogue.style.height = this._iHeightPx+'px';
		nDialogue.style.marginTop = parseInt(-1* this._iHeightPx/2)+'px';
	}
	
	if ( this._iWidthPx != null )
	{
		nDialogue.style.width = this._iWidthPx+'px';
		nDialogue.style.marginLeft = parseInt(-1* this._iWidthPx/2)+'px';
	}
	
	
	// Create background div
	var nBackground = document.createElement( "div" );
	nBackground.setAttribute('id', 'alert_background');
	nBackground.style.display = 'none'; // Hide until populated
	if ( this._sClass != '' )
	{
		nBackground.className = this._sClass;
	}
	document.body.insertBefore
		( nBackground, document.body.childNodes[0] );
	
	if ( this._sBackgroundColor != null )
	{
		nBackground.style.backgroundColor = this._sBackgroundColor;
	}
	
	if ( this._fBackgroundOpacity != null )
	{
		this._fnSetOpacity( nBackground, this._fBackgroundOpacity );
	}
	
	
	// Header
	if ( typeof this._sTitle != 'undefined' )
	{
		var nHeader = document.createElement( "div" );
		nHeader.setAttribute('id', 'alert_header');
		nDialogue.appendChild( nHeader );
	}
	
	// Content
	var nContent = document.createElement( "div" );
	nContent.setAttribute('id', 'alert_content');
	nDialogue.appendChild( nContent );
	
	// Buttons
	var nButtons = document.createElement( "div" );
	nButtons.setAttribute('id', 'alert_buttons');
	nDialogue.appendChild( nButtons );
};




