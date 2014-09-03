DriveInfoExFull
===============

获取计算机硬盘序列号

获取计算机硬盘序列号用途很多，在网上找到了一个C++的源代码DriveInfoEx(点这里查看)。非常好的一个DLL，.NET项目可以直接引用，而且源代码里有示例。

但这个DLL在Win7非管理员权限下，无法获取硬盘序列号，所以我就完善了一下这个DLL，让其支持Win7 非管理员。

源代码：

https://github.com/Xiongpq/DriveInfoExFull

编译时请注意

源代码内的一些方法，在VC90里已经被系统直接支持，所以就不用再重复定义，不然编译不过，所以如果在VC90及大于VC90平台编译的话，需要加一个“VC90”的“预处理器定义”。

代码中我做了判断，如果预定义了“VC90”就不会定义一些方法。在VC80及小于VC80平台编译的话，不用做这个设置。

DriveInfoExFull/DriveInfoEx/bin 目录下有已经编译好的DLL，这两个DLL支持.NET Framework 2.0
