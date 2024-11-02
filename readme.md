
# Lite CosmosDB Explorer

A cross-platform client explorer for Microsoft Azure CosmosDB (Windows, MacOS, Linux).

## Features

- Connect to Azure CosmosDB and display databases and containers
- Filter CosmosDB documents
- Create/Read/Update/Delete a document
- Colored JSON Editor for documents
- Execute a query and view results and RUs
- Save the last executed queries (they are saved automatically and available in a dropdown)

## How to Download
Win-x64, macOS-x64, macOS-arm64 and linux-x64 pre-built binaries can be found on Releases page.

## How to run
Windows
- Right-click -> Open

You can allow windows defender to start this application:

- click on More Info -> Run anyway

## macOS
- Make CosmosExplorer.Avalonia file executable by:

  chmod +x LiteCosmosExplorer

- Right-click -> Open

You can allow macOS to start this application by enabling Developer tools for Terminal:

System Preferences -> Security & Privacy -> Privacy, select "Developer Tools" on the left, check terminal on the right.



## Linux (CentOS, Debian, Fedora, Ubuntu and derivatives)
- Right-click -> Run

## How to Start
Add your connectrion string on tab Connections:
- Connection string name: The display name of your connection string
- Connection string: Copy PRIMARY KEY or SECONDARY CONNECTION STRING from the Keys menu on the Azure Portal.

az cosmosdb sql role assignment list \
--resource-group "HomeTraining" \
--account-name "hometrainingdb"

az cosmosdb show \
--resource-group "HomeTraining" \
--name "hometrainingdb" \
--query "{id:id}"

az cosmosdb sql role definition list --resource-group "HomeTraining" --account-name "hometrainingdb"

/subscriptions/9ef0db8a-83d1-44f0-8934-262b7a30fc28/resourceGroups/HomeTraining/providers/Microsoft.DocumentDB/databaseAccounts/hometrainingdb

az cosmosdb sql role assignment create --resource-group "HomeTraining" --account-name "hometrainingdb" --role-definition-id "00000000-0000-0000-0000-000000000002" --principal-id "98c8de5d-2771-4157-8306-dc1c9df1ae73" --scope "/subscriptions/9ef0db8a-83d1-44f0-8934-262b7a30fc28/resourceGroups/HomeTraining/providers/Microsoft.DocumentDB/databaseAccounts/hometrainingdb"

az cosmosdb sql role assignment create --resource-group "HomeTraining" --account-name "hometrainingdb" --role-definition-id "00000000-0000-0000-0000-000000000001" --principal-id "98c8de5d-2771-4157-8306-dc1c9df1ae73" --scope "/subscriptions/9ef0db8a-83d1-44f0-8934-262b7a30fc28/resourceGroups/HomeTraining/providers/Microsoft.DocumentDB/databaseAccounts/hometrainingdb"

az cosmosdb sql role assignment create --resource-group "HomeTraining" --account-name "hometrainingdb" --role-definition-id "00000000-0000-0000-0000-000000000001" --principal-id "0532732b-e2dc-4174-a49e-8a69df6ab5f3" --scope "/subscriptions/9ef0db8a-83d1-44f0-8934-262b7a30fc28/resourceGroups/HomeTraining/providers/Microsoft.DocumentDB/databaseAccounts/hometrainingdb"