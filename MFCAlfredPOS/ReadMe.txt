=============================================================================
To add the activex control to the main dialog:
1. Register assembly as:  regasm MpsWidgetHostingControl.dll /codebase /tlb:MpsWidgetHostingControl.tlb
2. Open the resource file
3. Open the dialog you would like to place the activex control on
4. Right click on the form and choose: insert activex control.
5. Select MpsWidgetHostingControl.MpsWidgetHostingControl
6. Resize the control to your preference
7. Right click on the form again and choose class wizard
8. Select the 'Add Class' button/drop down and then select MFC Class from ActiveX control
9. Select the radio button next to: Registry
10. Select MpsWidgetHostingControl.MpsWidgetHostingControl
11. Select IMpsWidgetControl in interfaces and then click the arrow button to move it to generated Classes
12. This should generate a class named CMpsWidgetControl
13. Click ok, and then finish, and you should see the new class .cpp and .h files were added to your project.
14. Switch back to the dialog view and right click on the activex control, choose 'add variable'.
15. Name the variable m_MobileWalletCtrl, leave everything else defaulted, and click finish.
16. Edit the header file of your main window's dialog class:  MFCAlfredPOSDlg.h in my case
17. Add the CMpsWidgetControl.h file to the includes
18. Change the type of m_MobileWalletCtrl from CAlfred1 to CMpsWidgetControl
19. Edit the implementation file of your main window's dialog class:  MFCAlfredPOSDlg.cpp in my case.
20. Copy this code below the // TODO: Add extra initialization here in OnInitDialog

	CString midKey("MERCHANTID");
	CString midValue("111111187942001=MISMIS");
	CString pwdKey("PASSWORD");
	CString pwdValue("xyz");
	CString whUrlKey("WHURL");	
	CString whUrlValue("https://wh.mercurycert.net/wallet");
	CString cmpUrlKey("COMPLETEURL");
	CString cmpUrlValue("http://localhost/test.html");
	CString amountKey("AMOUNT");
	CString amountValue("2.22");

	m_MobileWalletCtrl.AddAttribute(midKey, midValue);
	m_MobileWalletCtrl.AddAttribute(pwdKey, pwdValue);
	m_MobileWalletCtrl.AddAttribute(whUrlKey, whUrlValue);
	m_MobileWalletCtrl.AddAttribute(cmpUrlKey, cmpUrlValue);
	m_MobileWalletCtrl.AddAttribute(amountKey, amountValue);
	m_MobileWalletCtrl.Navigate();

21. Return to the dialog view and right click on the activex control
22. Choose 'Add Event Handler'
23. The add event handler dialog will show, just accept the defaults as there is only one event exposed and choose 'Add and Edit'
24. Copy this code into the event handler created:

	AfxMessageBox(data);

25. Compile and execute!
