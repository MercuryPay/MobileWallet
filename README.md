# MobileWallet
============

<a href="https://developer.vantiv.com/?utm_campaign=githubcta&utm_medium=hyperlink&utm_source=github&utm_content=gotquestions">Got questions? Connect with our experts on Vantiv ONE.</a>

The goal of this code is to provide an example of how to call the Mercury Mobile Wallet Hosting Service via an embedded web browser control.  This code uses the .net web browser control.

# PreReqs

The code was written using Visual Studio 2013 and c#.  The code should transfer between visual studio versions but if you want to compile immediately you will need 2013.

#Contents
The solution contains two projects:

1. ALFREDPOS -- a test "pos" that will drive the MpsWidgetHostingControl written in c#.
2. MPSWidgetHostingControl -- this is an activex control that uses the .net web browser control to facilitate a mobile wallet payment.

# Usage

1. Follow the steps in the integration guide to check a customer into a merchant location.
2. Compile the code
3. Run the ALFREDPOS application and click on the mobile wallet button.
4. You should see the customer checked in.


### ©2014 Mercury Payment Systems, LLC - all rights reserved.

Disclaimer:
This software and all specifications and documentation contained herein or provided to you hereunder (the "Software") are provided free of charge strictly on an "AS IS" basis. No representations or warranties are expressed or implied, including, but not limited to, warranties of suitability, quality, merchantability, or fitness for a particular purpose (irrespective of any course of dealing, custom or usage of trade), and all such warranties are expressly and specifically disclaimed. Mercury Payment Systems shall have no liability or responsibility to you nor any other person or entity with respect to any liability, loss, or damage, including lost profits whether foreseeable or not, or other obligation for any cause whatsoever, caused or alleged to be caused directly or indirectly by the Software. Use of the Software signifies agreement with this disclaimer notice.

![Analytics](https://ga-beacon.appspot.com/UA-60858025-33/MobileWallet.CSharp/readme?pixel)
