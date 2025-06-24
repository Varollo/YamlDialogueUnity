<div align="center">
 <img width=128px height=128px src="https://raw.githubusercontent.com/Varollo/YamlDialogueLib/refs/heads/master/Resources/Package/Common/icon.png" alt="Project logo">

# YAML Dialogue Unity

 An interpreter library for .yaml files to use in your Unity games.

[!![GitHub Release](https://img.shields.io/github/v/release/Varollo/YamlDialogueLib)
](https://github.com/Varollo/YamlDialogueLib/releases)
[![NuGet Version](https://img.shields.io/nuget/v/YamlDialogueLib)](https://www.nuget.org/packages/YamlDialogueLib/)
[![License](https://img.shields.io/badge/license-MIT-lime.svg)](/LICENSE)

| [🧐 About ](#-about-) | [🏁 Getting Started ](#-getting-started-) | [🎈 Usage ](#-usage-) |
|-|-|-|
</div>

## 🧐 About <a name = "about"></a>

This is a *Unity* package for personal use in my ~~(and yours, if you wish)~~ games. It interprets a `.yaml` file in this custom [schema](https://github.com/Varollo/YamlDialogueLib/blob/master/schema.json) into C# and provides a customizable plug-and-play UI with the use of prefabs.

This package also supports **DOTween** as it's means of animating the user interface.

## 🏁 Getting Started <a name = "getting_started"></a>

You can simply download the package and get the dependencies, then install it in your project and be good to go.

### Prerequisites

| Package | Requirement |
| --- | --- |
| ✅ [YamlDotNet](assetstore.unity.com/packages/p/yamldotnet-for-unity-36292) | Always required. |
| ℹ️ [DOTween](assetstore.unity.com/packages/p/dotween-hotween-v2-27676) | Only for DOTween module. |

You'll need to install the YamlDotNet package, from the Unity Asset Store.

If you choose to install the DOTween module, you'll aso need to get it from the Asset Store.

### Installing

#### 1. Install YamlDotNet

Make sure you have installed [YamlDotNet](assetstore.unity.com/packages/p/yamldotnet-for-unity-36292) through the Unity Asset store. This is the only obligatory requirement, everything else is optional, depending on what you need for your project.

#### 2. (OPTIONAL) Install DOTween

if you want to use the [DOTween](assetstore.unity.com/packages/p/dotween-hotween-v2-27676) module, you'll need to install it aswell.

Make sure you create an Assembly Definition file through the *DOTween Utility Panel*.

If the panel doesn't pop up on it's own, you can open it through the menu: 

> Tools/Demigiant/DOTween Utility Panel

Then, if you've not done it already, click the `Create ASMDEF` button.

#### 3. Install YamlDialogueUnity

Provided you got all prerequisites, you are ready to install the package. To do that, there're 2 ways, through Git URL or downloading it directly from the [Github Releases Page](https://github.com/Varollo/YamlDialogueUnity/releases).

The **recomended** way is installing it through the Git URL, since it makes it easier to keep the package updated.

First, copy the following git URL:

```
https://github.com/Varollo/YamlDialogueUnity.git
```

Then, open the *package manager window*, through the menu:

> Windows/Package Manager

Then, on the **top left** of the *package manager*, click the `+` icon and, on the dropdown menu, select the option `Install package from git URL...`, paste the URL copied and click `install`.

If you don't want to use the optional *DOTween module*, uncheck the corresponding folder in Unity's package import window, that being:

> Modules/DOTween

You should be ready to go, but I recommend checking out an example or two, to have an idea of how the package can be used.

### Examples

Inside the package you'll find 4 example scenes, a combination of both *input systems* and both *simple* or *DOTween* modules.

Make sure to only import the example that fits your project's needs best.

## 🎈 Usage <a name="usage"></a>

The package comes with a plug-and-play prefab you can place in your scene and modify to suit your project.

You'll only need to provide the prefab with an `ActorDatabase` scriptable object reference.

### The Actor Database

The **actor database** is a scriptable object used to associate an *actor name* with a *sprite*. You can create an instance of it by **right-clicking** the *project* window and navigating to:

> Create/YamlDialogueUnity/Actor Image Database

In theory, only one database is required, but nothing stops you from having as many as you need.

### The Actor View

The package has an option to display an image of the actors when they speak. The image needs to be stored in the **actor database** and associated with the **actor name**.

The provided prefab and DialogueView allows for 0, 1 or 2 actors to be shown at the same time, you can change this setting in the inspector for the DialogueView itself, under `Max Actors`.

You can also trigger certain events through the use of `actions` inside your .yaml file.

| Action | Effect |
| --- | --- |
| `actorview_clear` | Clears all actors on screen |

> ℹ️ **INFO**
> To know more about the .yaml file and it's capabilities, see the [lib repository](https://github.com/Varollo/YamlDialogueLib).

To have more than 2 actors and further controll over how they are shown, you'll need to extend the DialogueView and DialogueActorView included in the package.

### Creating a custom DialogueView

If you wish to make a custom `DialogueView`, you can extend `DialogueViewBase`.

You'll need to implement 5 abstract methods:

```cs
using YamlDialogueUnity;

public class DialogueViewExample : DialogueViewBase
{
    // inspector fields
    public DialogueActorView actorView;
    public DialogueOptionView optionPrefab;
    public Transform optionsHolder;
    public ActorDatabaseSO actorDatabase;
    public int maxActors;

    // Instantiate controller, passing this view and the actor view.
    protected override DialogueController CreateController() 
    {
        return new DialogueController(this, actorView);
    }

    // instantiate options handler, passing the prefab and the parent to hold it's instances.
    public override DialogueOptionsHandler CreateOptionsHandler() 
    {
        return new DialogueOptionsHandler(optionPrefab, optionsHolder);
    }

    // scriptable object reference to actor sprites.
    public override ActorDatabaseSO GetActorDatabase()
    {
        return actorDatabase;
    }

    // maximum ammount of actors to show at the smae time.
    public override int GetMaxActors()
    {
        return maxActors;
    }

    // called every step of the dialogue, used to update text.
    public override void UpdateView(string actor, string line, string[] actions) 
    {
        Debug.Log($"{actor}: {line}");
    }
}
```

Alternatively, you can extend directly from `DialogueView`, and just override the virtual methods:

```cs
using YamlDialogueUnity;

public class DialogueViewExample : DialogueView
{
    // called when EventSystem fires an "OnCancel" event.
    public override void OnCancel()
    {
        Debug.Log("Canceled.");
    }

    // called when input focus moves to another view, such as options.
    public override void OnDeselect()
    {
        Debug.Log("No longer selecting view.");
    }

    // called when dialogue window closes.
    public override IEnumerator OnHide()
    {
        Debug.Log("Goodbye...");
    }

    // called when input focus moves to main dialogue view.
    public override void OnSelect()
    {
        Debug.Log("Now selecting view!");
    }

    // called when dialogue window opens.
    public override IEnumerator OnShow()
    {
        Debug.Log("Hello!");
    }

    // called when EventSystem fires an "OnSubmit" event.
    // base method calls "Next", to advance dialogue.
    public override void OnSubmit()
    {
        base.OnSubmit();
    }

    // called on next dialogue step, used to set the actor name text
    protected override void SetActorTxt(string actor, TMP_Text actorTxt)
    {
        base.SetActorTxt(actor, actorTxt);
    }

    // called on next dialogue step, used to set the line text
    protected override void SetLineTxt(string line, TMP_Text lineTxt)
    {
        base.SetLineTxt(line, lineTxt);
    }
}
```