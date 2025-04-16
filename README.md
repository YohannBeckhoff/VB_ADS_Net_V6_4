# 📘 Lecture de variables ADS en Visual Basic (.NET) – TwinCAT 3

Ce projet est une **application Windows Forms en Visual Basic (.NET)** permettant de lire des variables depuis un automate **Beckhoff TwinCAT 3** via le protocole **ADS**.

---

## 🧰 Fonctionnalités

- Connexion automatique au runtime TwinCAT local
- Lecture cyclique (toutes les 50 ms) des variables suivantes :
  - `Main.nCycleTime` (UDINT) – temps de cycle de tâche
  - `Main.wCount` (WORD) – compteur ou valeur numérique
  - `Main.s1024String` (STRING(1024)) – chaîne de caractères
- Affichage en temps réel dans une interface graphique Windows Forms

---

## 🧩 Dépendances

- [.NET Framework](https://dotnet.microsoft.com/en-us/) (ou .NET Core compatible WinForms)
- [Beckhoff TwinCAT.Ads.dll](https://infosys.beckhoff.com/)
- Références .NET :
  - `System.Windows.Forms`
  - `System.Text`

---

## 🖥️ Interface utilisateur

L’interface comprend plusieurs `Label` pour afficher les valeurs lues :

- `Label2` → Affiche la valeur de `Main.nCycleTime`
- `Label3` → Affiche la valeur de `Main.wCount`
- `Label7` → Affiche le texte de `Main.s1024String`
- `Label9` → Affiche la longueur (`Len()`) de `Main.s1024String`

---

## 🔄 Fonctionnement

### Lecture ADS via `TcAdsClient`

1. Connexion au runtime local :  
   `adsClient.Connect(851)`

2. Création d’un handle pour chaque variable lue :  
   `CreateVariableHandle("Main.nomVariable")`

3. Lecture des données :
   - `ReadAny(handle, GetType(...))` pour les types numériques
   - `ReadAnyString(handle, 1024, Encoding)` pour la chaîne `STRING(1024)`

4. Suppression du handle :
   - `DeleteVariableHandle(handle)`

5. Répétition automatique via un `Timer` (`Interval = 50 ms`)

---

## 💡 Exemple de variables PLC attendues

Assurez-vous que ces variables existent dans votre code automate :

```pascal
VAR
    nCycleTime : UDINT; (* Ex: temps de cycle de tâche en ns *)
    wCount     : WORD;  (* Compteur *)
    s1024String: STRING(1024); (* Chaîne longue *)
END_VAR
