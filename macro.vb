Sub GetData()
    Dim ibmCurrentTerminal As IbmTerminal
    Dim ibmCurrentScreen As IbmScreen
    Dim hiddenTextEntry As String
    Dim returnValue As Integer
    Dim timeout As Integer
    Dim waitText As String
    Dim dataText As String
    Dim scrapedText As String
    timeout = 15000
    
    Dim fso As Object
    Set fso = CreateObject("Scripting.FileSystemObject")
    Dim oFile As Object
    Set oFile = fso.CreateTextFile("U:\")
    oFile.Write
    oFile.Close
    Set fso = Nothing
    Set oFile = Nothing

    Dim AppChannel = DDEInitiate(App:="okdhs-live-util-app", Topic:="ims");
    
    
    DDEExecute(Channel:=AppChannel, Command:="")
    'DDETerminate Channel:=AppChannel
    'DDEPoke Channel:=AppChannel, Item:="R1C1", Data:="1996 Sales" 
    'DDETerminate Channel:=AppChannel

    DDDDERequest(Channel:=AppChannel , Item:="" )


    Set ibmCurrentTerminal = ThisFrame.SelectedView.control
    Set ibmCurrentScreen = ibmCurrentTerminal.screen
    
    ' Clear screen and go to EA
    ibmCurrentScreen.SendControlKey (ControlKeyCode_Clear)
    ibmCurrentScreen.SendKeys ("EA")
    ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
    
    returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
    waitText = "NEXT"
    
    ' Cycle through screens and grab text
    While ibmCurrentScreen.GetText(1, 78, 2) <> "EF"
        returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 2, TextComparisonOption_MatchCase)
        scrapedText = scrapedText & "," & ibmCurrentScreen.GetTextEx(1, 1, 24, 80, GetTextArea_Block, GetTextWrap_Off, GetTextAttr_Visible, GetTextFlags_CRLF)
        ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
        returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
    Wend
    returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 2, TextComparisonOption_MatchCase)
    
    MsgBox ("e screens read")
    
    'Go to bn screen and cycle
    
    ibmCurrentScreen.SendKeys ("bn")
    ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
    returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
    returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 1, TextComparisonOption_MatchCase)
    scrapedText = scrapedText & ibmCurrentScreen.GetTextEx(1, 1, 24, 80, GetTextArea_Block, GetTextWrap_Off, GetTextAttr_Visible, GetTextFlags_CRLF)
    
    ' Stop when data is at certain screen position
    
    While ibmCurrentScreen.GetText(22, 20, 4) <> "DATA"
        returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 1, TextComparisonOption_MatchCase)
        scrapedText = scrapedText & "," & ibmCurrentScreen.GetTextEx(1, 1, 24, 80, GetTextArea_Block, GetTextWrap_Off, GetTextAttr_Visible, GetTextFlags_CRLF)
        ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
        returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
    Wend
    
    MsgBox ("bn screens read")
    
    'go to echl and cycle 22, 13
    ibmCurrentScreen.SendControlKey (ControlKeyCode_Clear)
    ibmCurrentScreen.SendKeys ("echl")
    ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
    returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
    
    
    While ibmCurrentScreen.GetText(22, 13, 3) <> "END"
        returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 2, TextComparisonOption_MatchCase)
        scrapedText = scrapedText & "," & ibmCurrentScreen.GetTextEx(1, 1, 24, 80, GetTextArea_Block, GetTextWrap_Off, GetTextAttr_Visible, GetTextFlags_CRLF)
        ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
        returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
    Wend
    
    MsgBox ("echl screens read")
    
    ibmCurrentScreen.SendControlKey (ControlKeyCode_Clear)
    ibmCurrentScreen.SendKeys ("cmm")
    ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
    returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
    scrapedText = scrapedText & "," & ibmCurrentScreen.GetTextEx(1, 1, 24, 80, GetTextArea_Block, GetTextWrap_Off, GetTextAttr_Visible, GetTextFlags_CRLF)
    
    While ibmCurrentScreen.GetText(22, 40, 2) <> "EA"
        returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 2, TextComparisonOption_MatchCase)
        scrapedText = scrapedText & "," & ibmCurrentScreen.GetTextEx(1, 1, 24, 80, GetTextArea_Block, GetTextWrap_Off, GetTextAttr_Visible, GetTextFlags_CRLF)
        ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
        returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
    Wend
    
    MsgBox ("cmm screens read")
    
    ibmCurrentScreen.SendControlKey (ControlKeyCode_Clear)
    ibmCurrentScreen.SendKeys ("nl")
    ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
    
    While ibmCurrentScreen.GetText(22, 28, 4) = "NEXT"
        returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 2, TextComparisonOption_MatchCase)
        scrapedText = scrapedText & "," & ibmCurrentScreen.GetTextEx(1, 1, 24, 80, GetTextArea_Block, GetTextWrap_Off, GetTextAttr_Visible, GetTextFlags_CRLF)
        ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
        returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
    Wend
    
    MsgBox ("nl screens read")
    
    returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 2, TextComparisonOption_MatchCase)
    scrapedText = scrapedText & "," & ibmCurrentScreen.GetTextEx(1, 1, 24, 80, GetTextArea_Block, GetTextWrap_Off, GetTextAttr_Visible, GetTextFlags_CRLF)
    
    MsgBox (scrapedText)
End Sub
'---------------------------------------------------------------------
' Generated by Micro Focus Reflection Desktop (17.0.434.0)
' Generated by the Macro Recorder on 10/13/2021 6:02:42 PM
'---------------------------------------------------------------------
' Common variable declarations
Dim ibmCurrentTerminal As IbmTerminal
Dim ibmCurrentScreen As IbmScreen
Dim hiddenTextEntry As String
Dim returnValue As Integer
Dim timeout As Integer
Dim waitText As String
Dim dataText As String
timeout = 15000

Set ibmCurrentTerminal = ThisFrame.SelectedView.control
Set ibmCurrentScreen = ibmCurrentTerminal.screen
'---------------------------------------------------------------------
ibmCurrentScreen.SendControlKey (ControlKeyCode_Clear)
'


returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
ibmCurrentScreen.SendKeys ("ea")
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'


returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
waitText = "NEXT"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 2, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendKeys ("eb")
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'


returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
waitText = "NEXT"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 2, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendKeys ("ec")
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'


returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
waitText = "NEXT"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 2, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendKeys ("ed")
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'



returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
waitText = "NEXT"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 2, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendKeys ("ee")
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'



returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
waitText = "NEXT"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 2, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendKeys ("bn")
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'


stopText = "DATA"

returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
waitText = "NEXT"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 1, TextComparisonOption_MatchCase)

While ibmCurrentScreen.GetText(22, 20, 4) <> data
    returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 1, TextComparisonOption_MatchCase)
    ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
    returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
EndWhile




ibmCurrentScreen.SendControlKey (ControlKeyCode_Clear)
ibmCurrentScreen.SendKeys ("echl")
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)


ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 23, 6)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
waitText = "NEXT"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 1, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 23, 6)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
waitText = "NEXT"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 1, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 23, 6)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
waitText = "NEXT"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 1, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 23, 6)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
waitText = "NEXT"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 1, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 23, 6)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
waitText = "NEXT"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 1, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 23, 6)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
waitText = "NEXT"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 1, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 23, 6)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
waitText = "NEXT"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 1, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 23, 6)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
waitText = "NEXT"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 1, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 23, 6)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
waitText = "NEXT"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 1, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 23, 6)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
waitText = "NEXT"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 1, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 23, 6)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
waitText = "NEXT"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 1, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 23, 8)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
waitText = "NEXT:"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 2, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 23, 8)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
waitText = "NEXT:"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 2, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 23, 8)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
waitText = "NEXT:"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 2, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendKeys ("echl")
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 8, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 8, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 8, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 8, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 8, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 8, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 8, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 8, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 8, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 8, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 8, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 8, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 8, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 8, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 8, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 8, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 1)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 8, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 8, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 8, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Clear)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 1, 1)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendKeys ("cmm")
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 23, 7)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
waitText = "NEXT"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 2, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 23, 7)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
waitText = "NEXT"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 2, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendKeys ("cmc")
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 23, 7)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
waitText = "NEXT"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 2, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendKeys ("nl")
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 7, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 7, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 7, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 7, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 7, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 7, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 7, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 7, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 7, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 7, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 7, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 7, 2)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendKeys ("bnp")
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 23, 8)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
waitText = "NEXT:"
returnValue = ibmCurrentScreen.WaitForText1(timeout, waitText, 23, 2, TextComparisonOption_MatchCase)
ibmCurrentScreen.SendKeys ("py")
ibmCurrentScreen.SendControlKey (ControlKeyCode_Transmit)
'
returnValue = ibmCurrentScreen.WaitForKeyboardEnabled(timeout, 0)
'Wait for cursor to be in position before continuing
returnValue = ibmCurrentScreen.WaitForCursor1(timeout, 7, 6)
If (returnValue <> ReturnCode_Success) Then
    Err.Raise 5001, "WaitForCursor1", "Timeout waiting for cursor position.", "VBAHelp.chm", "5001"
End If
ibmCurrentScreen.SendControlKey (ControlKeyCode_Clear)
'