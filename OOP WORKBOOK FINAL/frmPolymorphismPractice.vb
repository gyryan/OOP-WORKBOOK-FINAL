Imports System.Drawing

Public Class frmPolymorphismPractice
    Inherits Form

    Private ReadOnly animalInput As New TextBox With {.Location = New Point(24, 45), .Width = 220}
    Private ReadOnly outputLabel As New Label With {.Location = New Point(24, 130), .AutoSize = True}

    Public Sub New()
        Text = "Polymorphism Practice"
        ClientSize = New Size(400, 210)
        StartPosition = FormStartPosition.CenterParent
        Controls.Add(New Label With {.Text = "Animal type (Dog/Cat):", .Location = New Point(24, 20), .AutoSize = True})
        Controls.Add(animalInput)
        Dim speakButton As New Button With {.Text = "Call Speak", .Location = New Point(24, 85), .Width = 100}
        AddHandler speakButton.Click, AddressOf CallSpeak
        Controls.Add(speakButton)
        Controls.Add(outputLabel)
    End Sub

    Private Sub CallSpeak(sender As Object, e As EventArgs)
        Dim animalType As String = animalInput.Text.Trim()
        If animalType = "" Then
            outputLabel.Text = "Enter Dog or Cat."
            Return
        End If

        Dim animal As Animal
        Select Case animalType.ToLowerInvariant()
            Case "dog"
                animal = New Dog()
            Case "cat"
                animal = New Cat()
            Case Else
                animal = New Animal()
        End Select

        outputLabel.Text = animal.Speak()
    End Sub

    Private Class Animal
        Public Overridable Function Speak() As String
            Return "Animal makes a sound."
        End Function
    End Class

    Private Class Dog
        Inherits Animal
        Private ReadOnly name As String

        Public Sub New()
            name = "Dog"
        End Sub

        Public Sub New(name As String)
            Me.name = name
        End Sub

        Public Overrides Function Speak() As String
            Return "Dog barks."
        End Function
    End Class

    Private Class Cat
        Inherits Animal

        Public Overrides Function Speak() As String
            Return "Cat meows."
        End Function
    End Class
End Class
