# What is Myra

Here is Myra: [https://github.com/rds1983/Myra](https://github.com/rds1983/Myra)

Myra is a UI Library that's build to be easily intergated to any C# game engines. It comes with MonoGame/FNA/Stride support, but also gives you the ability to write your own integration, simply by implement a few interfaces.

# How to use this repo

1. Install `Myra.PlatformAgnostic` nuget package.
2. Copy the entire "MyraIntegration" folder to your Unigine project, put it under the source folder (or anywhere you like).
3. Modify file "MyraIntegration/MyraIntegration.cs", goto `MyraIntegration.Init()`, set your own root layout widgets to `Desktop.Root`.
4. Modify file "MyraIntegration/MyraPlatform.cs", goto `void IMyraPlatform.SetMouseCursorType(MouseCursorType mouseCursorType)`, set your own mouse cursor with `Input.SetMouseCursorCustom()`

And you're done!

# How these code works

I've written a few blog posts (in Simplified Chinese only) describing the details of almost every lines of code, including some pitfalls I ran into while making all this.

Unigine整合Myra UI Library全纪录：[https://www.cnblogs.com/horeaper/p/19110267](https://www.cnblogs.com/horeaper/p/19110267)

I'm not a Unigine master, in fact I only started using this engine, so if you spot any error, or any implementation fault, please fire an issue, or better, a pull request! ^_^

# Gallery

Myra's All Widgets Sample, with Unigine's default world scene:

![](/images/AllWidgets.jpg)
