Public Class Main
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Forms.Main.ListBox1.Items.Clear()
        MouseHook.lastActiveTime = Now
        MouseHook.printListBox = My.Forms.Main.ListBox1
        MouseHook.StartHook()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MouseHook.StopHook()
    End Sub
End Class
