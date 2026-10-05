Imports System.Drawing

Public Class frmSelectionRepetitionPractice
    Inherits Form

    Private ReadOnly clockTimer As New System.Windows.Forms.Timer With {.Interval = 1000}
    Private ReadOnly timeLabel As New Label With {.Location = New Point(30, 100), .Size = New Size(420, 78), .Font = New Font("Segoe UI", 34.0!, FontStyle.Bold), .ForeColor = Color.White, .TextAlign = ContentAlignment.MiddleCenter, .Text = "00:00"}
    Private ReadOnly statusLabel As New Label With {.Location = New Point(30, 188), .Size = New Size(420, 26), .Font = New Font("Segoe UI", 10.0!), .ForeColor = Color.FromArgb(190, 208, 228), .TextAlign = ContentAlignment.MiddleCenter, .Text = "Ready"}
    Private elapsedSeconds As Integer

    Public Sub New()
        Text = "Week 6 - Timer Practice"
        ClientSize = New Size(480, 340)
        StartPosition = FormStartPosition.CenterParent
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        BackColor = Color.FromArgb(11, 27, 49)
        ForeColor = Color.White

        Controls.Add(New Label With {.Text = "REPETITION TIMER", .Location = New Point(30, 28), .Size = New Size(420, 38), .Font = New Font("Segoe UI", 18.0!, FontStyle.Bold), .ForeColor = Color.White, .TextAlign = ContentAlignment.MiddleCenter})
        Controls.Add(timeLabel)
        Controls.Add(statusLabel)

        Dim startButton As New Button With {.Text = "Start", .Location = New Point(30, 250), .Size = New Size(125, 42), .BackColor = Color.FromArgb(37, 99, 157), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat, .Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)}
        Dim pauseButton As New Button With {.Text = "Pause", .Location = New Point(177, 250), .Size = New Size(125, 42), .BackColor = Color.FromArgb(37, 99, 157), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat, .Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)}
        Dim resetButton As New Button With {.Text = "Reset", .Location = New Point(325, 250), .Size = New Size(125, 42), .BackColor = Color.FromArgb(37, 99, 157), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat, .Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)}
        AddHandler startButton.Click, AddressOf StartTimer
        AddHandler pauseButton.Click, AddressOf PauseTimer
        AddHandler resetButton.Click, AddressOf ResetTimer
        AddHandler clockTimer.Tick, AddressOf ClockTimer_Tick
        AddHandler FormClosed, AddressOf TimerFormClosed
        Controls.Add(startButton)
        Controls.Add(pauseButton)
        Controls.Add(resetButton)
        Controls.Add(New Panel With {.Location = New Point(30, 226), .Size = New Size(420, 1), .BackColor = Color.FromArgb(70, 95, 124)})
    End Sub

    Private Sub StartTimer(sender As Object, e As EventArgs)
        clockTimer.Start()
        statusLabel.Text = "Timer running"
    End Sub

    Private Sub PauseTimer(sender As Object, e As EventArgs)
        clockTimer.Stop()
        statusLabel.Text = "Timer paused"
    End Sub

    Private Sub ResetTimer(sender As Object, e As EventArgs)
        clockTimer.Stop()
        elapsedSeconds = 0
        UpdateTimeDisplay()
        statusLabel.Text = "Ready"
    End Sub

    Private Sub ClockTimer_Tick(sender As Object, e As EventArgs)
        elapsedSeconds += 1
        UpdateTimeDisplay()
    End Sub

    Private Sub UpdateTimeDisplay()
        timeLabel.Text = (elapsedSeconds \ 60).ToString("00") & ":" & (elapsedSeconds Mod 60).ToString("00")
    End Sub

    Private Sub TimerFormClosed(sender As Object, e As FormClosedEventArgs)
        clockTimer.Stop()
        clockTimer.Dispose()
    End Sub
End Class
