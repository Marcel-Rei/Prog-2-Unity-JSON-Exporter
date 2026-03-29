# Prog 2 Level Editor Inside Unity

This is an export tool for Unity scenes which generates data compatible with the Prog 2 engine (DAE, second semester).  
Yes, you are allowed to use this for your Prog 2 game project.

## DISCLAIMER!
This exporter works for my use cases, but I can’t guarantee it will work perfectly in every project or scene, especially ones modified manually in the JSON. Please make sure to test it yourself before relying on it.

## Table of Contents

- [Disclaimer](#disclaimer)
- [Quick Start](#quick-start)
- [Installation](#installation)
- [Usage](#usage)
  - [Setup](#setup)
  - [How to open the export tool](#how-to-open-the-export-tool)
  - [Texture import](#texture-import)
  - [How to build a level](#how-to-build-a-level)
  - [Prefab examples](#prefab-examples)
    - [Pfb_Empty_Prog2](#pfb_empty_prog2)
    - [Pfb_Sprite_Prog2](#pfb_sprite_prog2)
    - [Pfb_Rectf_Prog2](#pfb_rectf_prog2)
    - [Pfb_Sprite_Rectf_Prog2](#pfb_sprite_rectf_prog2)
    - [Pfb_Custom_Data_Prog2](#pfb_custom_data_prog2)
  - [How to export custom data](#how-to-export-custom-data)
  - [How to export to JSON](#how-to-export-to-json)
  - [Export Settings](#export-settings)
- [FAQ](#faq)
- [Contributing](#contributing)
- [License](#license)

## Quick Start

1. Open the project in Unity (**6000.3.11f1 recommended**)
2. Create a new scene (this will be your export scene)
3. Open the tool: **Tool → Prog 2 Json Exporter**
4. Click **Prepare Unity Scene**
5. Add prefabs (e.g. `Pfb_Sprite_Prog2`) to your scene
6. (Optional) Place textures in **Assets/Prog2JsonExporter/Textures** and click **Clean Imported Textures**
7. Click **Export to JSON**

Your JSON file will be generated with the scene data ready for Prog 2.

---

## Installation

Pull the project and add it inside Unity Hub. Open the project with Unity version **6000.3.11f1**, but other versions will probably also work.  

Avoid Unity 6 versions before **6000.3.5f2**, because Newtonsoft JSON is used. See:  
https://docs.unity3d.com/6000.3/Documentation/Manual/upm-signature.html for more info.

---

# Usage

## Setup
After opening the project, create a new Unity scene and use this as your export scene.

## How to open the export tool 
You can find the export tool under **Tool → Prog 2 Json Exporter**

<img width="644" height="335" alt="image" src="https://github.com/user-attachments/assets/f7b5b404-ceac-4cd9-b1fc-41f2c00a9918" />

Click **Prepare Unity Scene** in the tool, or manually change the pivot mode to *Pivot* inside the Unity scene.

<img width="740" height="700" alt="image" src="https://github.com/user-attachments/assets/a283d99f-3fe3-406d-a5c3-877ec347c6ab" />

### If you hover over any setting, button, etc., it will describe what it does

---

## Texture import

Place any texture you want to use inside:  
**Assets/Prog2JsonExporter/Textures**

<img width="223" height="151" alt="image" src="https://github.com/user-attachments/assets/0340afe9-e21b-4dd0-b2b5-c098007daad2" />

Then open the tool and click **Clean Imported Textures**

<img width="746" height="687" alt="image" src="https://github.com/user-attachments/assets/afcaeb23-e378-46ac-86da-e025fe6643ad" />

This will automatically adjust and save texture settings so the textures match the Prog 2 engine.

---

## How to build a level

Simply add GameObjects with the **Prog 2 Object** component to your scene.  
Several prefab examples are provided for basic level creation.

---

## Prefab examples

### Pfb_Empty_Prog2

The base prefab which only exports position information

<img width="825" height="267" alt="image" src="https://github.com/user-attachments/assets/ec9af8b1-da0e-40d0-8b61-e2c5f6848ac3" />

Json Data example:
```json
{
  "xPosition": 0.0,
  "yPosition": 0.0
}
```

---

### Pfb_Sprite_Prog2

Exports position and texture information

<img width="825" height="570" alt="image" src="https://github.com/user-attachments/assets/d6cd963e-13e7-451f-9342-73d0c21857ee" />

```json
{
  "texturePath": "T_No_Texture_Assigned.png",
  "renderLayer": 0,
  "xPosition": 227.0,
  "yPosition": 224.0
}
```

---

### Pfb_Rectf_Prog2

Exports position and collider information (converted to Rectf format)

<img width="830" height="485" alt="image" src="https://github.com/user-attachments/assets/ea52dbb6-9d29-443d-be20-2bc580aac773" />

```json
{
  "xPosition": 0.0,
  "yPosition": 64.0,
  "prog2Rectf": {
    "left": -64.0,
    "bottom": -64.0,
    "width": 128.0,
    "height": 128.0
  }
}
```

---

### Pfb_Sprite_Rectf_Prog2

Exports position, collider, and sprite information

```json
{
  "texturePath": "T_No_Texture_Assigned.png",
  "renderLayer": 0,
  "xPosition": 48.0,
  "yPosition": 0.0,
  "prog2Rectf": {
    "left": 0.0,
    "bottom": 0.0,
    "width": 128.0,
    "height": 128.0
  }
}
```

---

### Pfb_Custom_Data_Prog2

Example of exporting custom data

<img width="823" height="581" alt="image" src="https://github.com/user-attachments/assets/ff3bae6b-f7ea-4151-b2cf-2e3e9850e5fb" />

```json
{
  "texturePath": "T_No_Texture_Assigned.png",
  "renderLayer": 0,
  "xPosition": 168.0,
  "yPosition": 0.0,
  "prog2Rectf": {
    "left": 0.0,
    "bottom": 0.0,
    "width": 128.0,
    "height": 128.0
  }
  "objectName": "MyObject",
  "layerName": "Default"
}
```

---

## How to export custom data 

To see an example, check:  
**Assets/Prog2JsonExporter/Scripts/Example**  
Also see the **Pfb_Custom_Data_Prog2** prefab.

### Step 1  
Create a class that inherits from **Prog2CustomData**.  

- Variables must be **public**
- Add **[Serializable]**

```csharp
[Serializable]
public class Prog2CustomDataExample : Prog2CustomData
{
    public string objectName;
    public string layerName;
}
```

### Step 2  
Create a component that inherits from **Prog2CustomObjectComponent**.

```csharp
public class Prog2ExampleCustomComponent : Prog2CustomObjectComponent
{
    [SerializeField] private Prog2CustomDataExample dataExample;
    
    public override Prog2CustomData GetCustomData()
    {
        if (String.IsNullOrEmpty(dataExample.objectName))
        {
            dataExample.objectName = gameObject.name;
        }
        
        dataExample.layerName = LayerMask.LayerToName(gameObject.layer);
        return dataExample;
    }
}
```

---

## How to export to JSON

Open the tool and click **Export to JSON**.  
You can change the filename in the filename field.

<img width="746" height="694" alt="image" src="https://github.com/user-attachments/assets/f75d6c17-ae85-4a92-8953-6827b4f1b22b" />

---

## Export Settings

- **Export Render Layer Info**  
  Exports the render layer from the Sprite Renderer

- **Export Is Trigger Info**  
  Exports whether a BoxCollider2D is set to *Is Trigger*

- **Round Down Collider Info to Nearest Int**  
  Useful because utils has issues drawing at .5 positions

- **Export Scene Owner Info**  
  Exports which scene the GameObject belongs to

- **Export Disabled Objects**  
  Includes disabled objects in the export

- **Ignore Sanity Check Warning**  
  Continues export even if warnings occur

- **Print Export Info in Console**  
  Prints progress info during export

---

## FAQ

### Am I allowed to use this for my Prog2 assignment?
Yes.

### Why is xyz not supported?
Because I didn’t need it for my own project.

### Can you add xyz feature?
Send me a message and I’ll look into it.

### Do you support other collider types?
No. If needed, ask and I’ll consider it.

### Do you support multiple texture types?
No. The tool is designed for users with little Unity knowledge.  
Supporting this would require explaining more complex Unity systems.

### How do I import the data in Prog2?
I’m not allowed to share that (graded C++ code).  
In general: use a JSON library to read the data.

### xyz is not working, what now?
Send me a message and I’ll look into it.

---

## Contributing

Pull requests are welcome. For major changes, open an issue first.

---

## License

[MIT](https://choosealicense.com/licenses/mit/)
