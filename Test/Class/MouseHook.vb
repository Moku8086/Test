Imports System.Runtime.InteropServices

Public Class MouseHook

    Private Const WH_MOUSE_LL = 14
    Private Const WM_LBUTTONDOWN = &H204

    Private Declare Function SetWindowsHookEx Lib "user32" Alias "SetWindowsHookExA" (ByVal idHook As Integer, ByVal lpfn As HookProc, ByVal hMod As IntPtr, ByVal dwThreadId As Integer) As Integer
    Private Declare Function UnhookWindowsHookEx Lib "user32" (ByVal idHook As Integer) As Integer
    Private Declare Function CallNextHookEx Lib "user32" (ByVal idHook As Integer, ByVal nCode As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As Integer
    Private Delegate Function HookProc(ByVal nCode As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As Integer
    Structure tagMSLLHOOKSTRUCT
        Public pt As Point
        Public mouseData As Integer
        Public flags As Integer
        Public time As Integer
        Public dwExtraInfo As Integer

    End Structure

    Shared hk As IntPtr
    Dim x, y As Integer

    Public Shared Sub StartHook()
        hk = SetWindowsHookEx(WH_MOUSE_LL, AddressOf MouseProc, Marshal.GetHINSTANCE(GetType(MouseHook).Module), 0)
    End Sub

    Public Shared Sub StopHook()
        UnhookWindowsHookEx(hk)
    End Sub

    Public Shared Function MouseProc(ByVal nCode As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As Integer
        If wParam = WM_LBUTTONDOWN Then
            Dim temstur As New tagMSLLHOOKSTRUCT
            temstur = CType(Marshal.PtrToStructure(lParam, GetType(tagMSLLHOOKSTRUCT)), tagMSLLHOOKSTRUCT)
            MsgBox(temstur.pt.X)
        End If

        Return CallNextHookEx(hk, nCode, wParam, lParam)
    End Function
End Class
