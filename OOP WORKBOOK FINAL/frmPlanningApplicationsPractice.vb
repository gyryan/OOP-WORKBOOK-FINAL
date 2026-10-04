Imports System.Drawing
Imports System.Globalization

Public Class frmPlanningApplicationsPractice
    Inherits Form

    Private ReadOnly textInput As New TextBox With {.Location = New Point(24, 48), .Width = 300}
    Private ReadOnly transformInput As New ComboBox With {.Location = New Point(24, 108), .Width = 180, .DropDownStyle = ComboBoxStyle.DropDownList}
    Private ReadOnly outputLabel As New Label With {.Location = New Point(24, 205), .AutoSize = True, .ForeColor = Color.White}

    Public Sub New()
        Text = "Planning Applications and Interfaces Practice"
        ClientSize = New Size(460, 270)
        StartPosition = FormStartPosition.CenterParent
        BackColor = Color.FromArgb(18, 40, 72)
        ForeColor = Color.White

        Controls.Add(New Label With {.Text = "Enter text:", .Location = New Point(24, 23), .AutoSize = True, .ForeColor = Color.White})
        Controls.Add(textInput)
        Controls.Add(New Label With {.Text = "Action:", .Location = New Point(24, 83), .AutoSize = True, .ForeColor = Color.White})
        transformInput.Items.AddRange(New Object() {"Uppercase", "Lowercase", "Title Case"})
        transformInput.SelectedIndex = 0
        Controls.Add(transformInput)

        Dim applyButton As New Button With {.Text = "Apply", .Location = New Point(24, 155), .Width = 100, .BackColor = Color.FromArgb(27, 75, 125), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        AddHandler applyButton.Click, AddressOf ApplyAction
        Controls.Add(applyButton)
        Controls.Add(outputLabel)
    End Sub

    Private Sub ApplyAction(sender As Object, e As EventArgs)
        Dim inputText As String = textInput.Text
        If String.IsNullOrWhiteSpace(inputText) Then
            outputLabel.Text = "Enter text to process."
            Return
        End If

        Select Case CStr(transformInput.SelectedItem)
            Case "Uppercase"
                outputLabel.Text = inputText.ToUpper(CultureInfo.CurrentCulture)
            Case "Lowercase"
                outputLabel.Text = inputText.ToLower(CultureInfo.CurrentCulture)
            Case "Title Case"
                outputLabel.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(inputText.ToLower(CultureInfo.CurrentCulture))
            Case Else
                outputLabel.Text = "Select an action."
        End Select
    End Sub
End Class
