Projet EasySave de ProSoft : 

Bienvenue dans EasySave, votre outil de sauvegarde simple et efficace. 
Ce guide vous aidera à créer et gérer vos travaux de sauvegarde. 
* Suivez ces étapes pour tirer le meilleur parti de l'application.

EasySave vous donne la flexibilité de créer plusieurs sauvegardes, que ce soit des sauvegardes complètes (option 1);
englobant l'intégralité des répertoires sources vers des répertoires cibles, ou des sauvegardes différentielles (option 2);
ne copiant que les fichiers modifiés depuis la dernière sauvegarde. 
EasySave permet également de crypter les fichiers d'extensions définis avec la méthode XOR.
  

Avant de démarrer, assurez-vous de préparer l'environnement en créant un répertoire dédié "C:/LOGJ", destiné à accueillir 
les logs journaliers consignant le suivi précis de vos opérations de sauvegarde.
Vous pouvez définir des logiciels métiers à ne pas copier dans le fichier "C:/LOGJ/logicielmetier.txt".

Vous trouverez l'application dans ".../bin/Debug/net8.0-windows/EasySavev2.exe".

Dans cette application, vous avez la possibilité d'effectuer plusieurs sauvegardes simultanément. Pour configurer vos paramètres de sauvegarde, cliquez sur "Paramètres" dans la fenêtre principale (MainWindow), ce qui ouvrira une autre fenêtre. Dans cette fenêtre de paramètres (Window1), vous pouvez choisir la tâche de sauvegarde, spécifier les extensions de fichier par priorité et définir d'autres préférences.

Pour lancer une sauvegarde, retournez à la fenêtre principale et cliquez sur "Enregistre". Cette action vous redirigera vers Window1, où vous pourrez saisir les informations nécessaires. Sélectionnez les extensions de fichier que vous souhaitez crypter. Si vous préférez ne pas crypter, choisissez  "Aucune". De plus, vous devez spécifier le type de sauvegarde : 1 pour complète et 2 pour différentielle. Choisissez le format de journal, soit XML soit JSON.

Une fois que vous avez rempli les informations requises, cliquez sur "Commence". Vous serez alors redirigé vers la Window4, où vous devrez décider si vous souhaitez effectuer une autre sauvegarde. Si vous choisissez "Oui", vous serez redirigé vers Window1 pour fournir de nouvelles informations. Si vous sélectionnez "Non", la première tâche de sauvegarde commencera, et la Window3 apparaîtra, vous permettant de consulter les journaux et l'état actuel en cliquant sur les boutons respectifs.

***********************






***********************

EasySave Project by ProSoft:

Welcome to EasySave, your simple and effective backup tool. 
This guide will help you create and manage your backup tasks. 
* Follow these steps to make the most of the application.

EasySave gives you the flexibility to create multiple backups, whether they are full backups (option 1) encompassing 
entire source directories to target directories or differential backups (option 2), only copying files modified since the last backup.
EasySave also allows you to encrypt files with the chosen extensions with the XOR method.


Before you begin, make sure to prepare the environment by creating a dedicated directory "C:/LOGJ," designed to store daily logs tracking
the precise progress of your backup operations.
You can define the work apps you do not want to copy in the file "C:/LOGJ/logicielmetier.txt".

You will find the app in ".../bin/Debug/net8.0-windows/EasySavev2.exe".


In this application, you have the capability to perform multiple backups simultaneously. To configure your backup settings, click on "Settings" in the MainWindow, which will open another window. In this Settings window (Window1), you can choose the backup job, specify file extensions by priority, and set other preferences.

To initiate a backup, return to the MainWindow and click on "Save." This action will redirect you to Window1, where you can input the necessary information. Select the file extensions you wish to encrypt. If you prefer not to encrypt, choose either "Nothing"  Additionally, you must specify the type of backup: 1 for complete and 2 for differential. Choose the log format, either XML or JSON.

Once you have filled in the required information, click "Start" You will then be redirected to the Window4, where you will be prompted to decide whether you would like to perform another backup. If you choose "Yes," you will be redirected to Window1 to provide new information. If you select "No," the first backup job will commence, and the Window3 will appear, allowing you to view logs and the current state by clicking on the respective buttons.