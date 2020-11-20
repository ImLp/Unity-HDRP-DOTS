# Unity HDRP + DOTS & StyleCop

A blank Unity3d 3D project you can use to start your own game / prototype while
ensuring that you follow good dev practices. This project uses the following packages:

- HDRP
- DOTS
- StyleCop.Analyzer

It also contains:

- A `.gitignore` targeting Unity, Mac, Windows, Rider & C# common ignore patters.
- A `.gitattribute` prepopulated to handle the most common file handling settings
for git.
- *.DotSetting pre-populated with syntax & style guidelines for Rider & Resharper
so you can spend less time setting up and more time creating your project.


---

[TOC]

---



## In-Depth project setup

If you were to replicate this package from a base Unity3D project:

### Added Packages

#### HDRP

Unity's High definition rendering pipeline targeting high-end PC, high-end Mac,
and high-end consoles for the purpose of creating high-definiton and photo-
realistic visuals. For more info check this out: [HDRP](https://blogs.unity3d.com/2018/09/24/the-high-definition-render-pipeline-getting-started-guide-for-artists/)

#### DOTS

Unity’s new high-performance, multithreaded Data-Oriented Technology Stack.
Allowing you to use Entity-Component-System based programming.
For more information check out [DOTS](https://unity.com/dots)

#### StyleCop.Analyzer

A custom made plugin that will automatically bring in Unity friendly style guidelines
to the project. Unity's default programming style is often in direct contradiction
of proper C# programming approaches. Often developers would ignore encapsulation,
proper documentation, and undertake other code-rotting practices just because they
are following the examples provided by Unity itself.

You can add this plugin by cloning the following repository: [https://github.com/ImLp/Unity-StyleCop])(https://github.com/ImLp/Unity-StyleCop)



