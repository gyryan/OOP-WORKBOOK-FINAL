Imports System.Drawing

Public Class frmSelectionRepetitionPractice
    Inherits Form

    Private ReadOnly ageInput As New TextBox With {.Location = New Point(24, 48), .Width = 180}
    Private ReadOnly outputLabel As New Label With {.Location = New Point(24, 140), .AutoSize = True, .ForeColor = Color.White}

    Public Sub New()
        Text = "Selection and Repetition Practice"
        ClientSize = New Size(440, 210)
        StartPosition = FormStartPosition.CenterParent
        BackColor = Color.FromArgb(18, 40, 72)
        Controls.Add(New Label With {.Text = "Enter your age:", .Location = New Point(24, 23), .AutoSize = True, .ForeColor = Color.White})
        Controls.Add(ageInput)

        Dim runButton As New Button With {.Text = "Run", .Location = New Point(24, 95), .Width = 90, .BackColor = Color.FromArgb(27, 75, 125), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        AddHandler runButton.Click, AddressOf RunPractice
        Controls.Add(runButton)
        Controls.Add(outputLabel)
    End Sub

    Private Sub RunPractice(sender As Object, e As EventArgs)
        Dim age As Integer
        If Not Integer.TryParse(ageInput.Text, age) OrElse age < 0 Then
            outputLabel.Text = "Enter a valid, non-negative age."
            Return
        End If

        If age < 18 Then
            outputLabel.Text = $"At age {age}, you are a minor."
        Else
            outputLabel.Text = $"At age {age}, you are an adult."
        End If
    End Sub
End Class
