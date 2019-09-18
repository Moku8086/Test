Imports System.Runtime.InteropServices

Public Class MouseHook

#Region "-- 常數 --"
    Private Const WH_MOUSE_LL = 14

    Private Const WM_MOUSEMOVE = &H200
    Private Const WM_LBUTTONDOWN = &H201
    Private Const WM_LBUTTONUP = &H202
    Private Const WM_RBUTTONDOWN = &H204
    Private Const WM_RBUTTONUP = &H205
#End Region

#Region "-- API --"
    Private Declare Function SetWindowsHookEx Lib "user32" Alias "SetWindowsHookExA" (ByVal idHook As Integer, ByVal lpfn As HookProc, ByVal hMod As IntPtr, ByVal dwThreadId As Integer) As Integer
    Private Declare Function UnhookWindowsHookEx Lib "user32" (ByVal idHook As Integer) As Integer
    Private Declare Function CallNextHookEx Lib "user32" (ByVal idHook As Integer, ByVal nCode As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As Integer
    Private Delegate Function HookProc(ByVal nCode As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As Integer
#End Region

#Region "-- 結構 --"
    Structure MSLLHOOKSTRUCT
        Public pt As Point
        Public mouseData As Integer
        Public flags As Integer
        Public time As Integer
        Public dwExtraInfo As Integer
    End Structure
#End Region

#Region "-- 變數 --"
    Shared hookHwnd As Integer
    Shared mousePoint As Point
    Public Shared printListBox As ListBox
    Shared tempStruct As MSLLHOOKSTRUCT
    Public Shared lastActiveTime As DateTime
#End Region

#Region "-- Main --"
    Public Shared Sub StartHook()
        hookHwnd = SetWindowsHookEx(WH_MOUSE_LL, AddressOf MouseProc, Marshal.GetHINSTANCE(GetType(MouseHook).Module), 0)
    End Sub

    Public Shared Sub StopHook()
        UnhookWindowsHookEx(hookHwnd)
    End Sub

    Public Shared Function MouseProc(ByVal nCode As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As Integer
        tempStruct = CType(Marshal.PtrToStructure(lParam, GetType(MSLLHOOKSTRUCT)), MSLLHOOKSTRUCT)


        Select Case wParam
            Case WM_MOUSEMOVE And Now.Subtract(lastActiveTime).TotalMilliseconds > 50
                printListBox.Items.Add("MOVE    |   x：" & tempStruct.pt.X & "   |   y：" & tempStruct.pt.Y)
                lastActiveTime = Now

            Case WM_LBUTTONDOWN
                printListBox.Items.Add("LBUTTONDOWN |   x：" & tempStruct.pt.X & "   |   y：" & tempStruct.pt.Y)

            Case WM_LBUTTONUP
                printListBox.Items.Add("LBUTTONUP   |   x：" & tempStruct.pt.X & "   |   y：" & tempStruct.pt.Y)

            Case WM_RBUTTONDOWN
                printListBox.Items.Add("RBUTTONDOWN |   x：" & tempStruct.pt.X & "   |   y：" & tempStruct.pt.Y)

            Case WM_RBUTTONUP
                printListBox.Items.Add("RBUTTONUP   |   x：" & tempStruct.pt.X & "   |   y：" & tempStruct.pt.Y)

        End Select

        'lastActive = wParam


        printListBox.TopIndex = printListBox.Items.Count - 1
        Return CallNextHookEx(hookHwnd, nCode, wParam, lParam)
    End Function
#End Region
End Class
