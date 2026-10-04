Imports System.Drawing

Public Class frmClassesAndObjectsPractice
    Inherits Form

    Private ReadOnly nameInput As New TextBox With {.Location = New Point(24, 45), .Width = 260}
    Private ReadOnly ageInput As New TextBox With {.Location = New Point(24, 95), .Width = 260}
    Private ReadOnly outputLabel As New Label With {.Location = New Point(24, 190), .AutoSize = True}

    Public Sub New()
        Text = "Classes and Objects Practice"
        ClientSize = New Size(430, 270)
        StartPosition = FormStartPosition.CenterParent

        Controls.Add(New Label With {.Text = "Name:", .Location = New Point(24, 20), .AutoSize = True})
        Controls.Add(nameInput)
        Controls.Add(New Label With {.Text = "Age:", .Location = New Point(24, 70), .AutoSize = True})
        Controls.Add(ageInput)
        Dim createButton As New Button With {.Text = "Create Person", .Location = New Point(24, 135), .Width = 130}
        AddHandler createButton.Click, AddressOf CreatePerson
        Controls.Add(createButton)
        Controls.Add(outputLabel)
    End Sub

    Private Sub CreatePerson(sender As Object, e As EventArgs)
        Dim age As Integer
        If nameInput.Text.Trim() = "" OrElse Not Integer.TryParse(ageInput.Text, age) OrElse age < 0 Then
            outputLabel.Text = "Enter a name and a valid age."
            Return
        End If

        Dim person As New Person With {.Name = nameInput.Text.Trim(), .Age = age}
        outputLabel.Text = $"Object created: {person.Name}, age {person.Age}."
    End Sub

    Private Class Person
        Public Property Name As String
        Public Property Age As Integer
    End Class
End Class
