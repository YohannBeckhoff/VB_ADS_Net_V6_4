Imports System.Reflection.Emit
Imports TwinCAT.Ads
Imports TwinCAT.TypeSystem



Public Class Form1

    'Déclaration du timer
    Private WithEvents monTimer As New Timer()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Intervalle en millisecondes (300ms = 0.3 seconde)
        monTimer.Interval = 50

        ' Démarre le timer
        monTimer.Start()
    End Sub

    ' Code qui sera exécuté à chaque "tick" (ici, chaque seconde)
    Private Sub monTimer_Tick(sender As Object, e As EventArgs) Handles monTimer.Tick




        ' Crée un client ADS
        Using adsClient As New TwinCAT.Ads.AdsClient()


            Try
                ' Connexion au runtime local (851 = port du runtime 1)
                adsClient.Connect(851)

                ' Lecture de la variable système
                ' ATTENTION : _TaskInfo est une structure système, il faut la lire comme un bloc de données ou passer par un type défini
                Dim handle As Integer = adsClient.CreateVariableHandle("Main.nCycleTime") '"_TaskInfo[1].CycleTime"

                ' Lecture de la valeur (c'est un UDINT, donc UInt32 en .NET)
                Dim cycleTime As UInteger = CUInt(adsClient.ReadAny(handle, GetType(UInteger)))
                Label2.Text = cycleTime
                Console.WriteLine("CycleTime : " & cycleTime.ToString() & " ns")

                ' Libère le handle
                adsClient.DeleteVariableHandle(handle)

                handle = adsClient.CreateVariableHandle("Main.wCount") '"_TaskInfo[1].CycleTime"

                ' Lecture de la valeur (c'est un UDINT, donc UInt32 en .NET)
                Dim wCount As UInteger = CUInt(adsClient.ReadAny(handle, GetType(UShort)))
                Label3.Text = wCount
                Console.WriteLine("wCount : " & wCount.ToString())

                ' Libère le handle
                adsClient.DeleteVariableHandle(handle)

                'Lecture de la chaine de caratères
                ' Créer un tampon pour 1024 caractères + terminaison nulle
                handle = adsClient.CreateVariableHandle("Main.s1024String")

                ' Lire la chaîne (max 1024 caractères)
                Dim result As String = adsClient.ReadAnyString(handle, 1024, StringMarshaler.DefaultEncoding)

                ' Affiche la valeur lue
                Console.WriteLine("Valeur lue : " & result)
                Dim lenResult = Len(result)

                Label7.Text = result
                Label9.Text = lenResult
                adsClient.DeleteVariableHandle(handle)


            Catch ex As Exception
                Console.WriteLine("Erreur : " & ex.Message)
            End Try
        End Using
    End Sub


End Class
