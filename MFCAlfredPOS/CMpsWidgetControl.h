// CMpsWidgetControl.h  : Declaration of ActiveX Control wrapper class(es) created by Microsoft Visual C++

#pragma once

/////////////////////////////////////////////////////////////////////////////
// CMpsWidgetControl

class CMpsWidgetControl : public CWnd
{
protected:
	DECLARE_DYNCREATE(CMpsWidgetControl)
public:
	CLSID const& GetClsid()
	{
		static CLSID const clsid
			= { 0xCBC190DB, 0xE536, 0x496D, { 0x9D, 0x30, 0xD1, 0x7A, 0x92, 0x78, 0x21, 0x26 } };
		return clsid;
	}
	virtual BOOL Create(LPCTSTR lpszClassName, LPCTSTR lpszWindowName, DWORD dwStyle,
						const RECT& rect, CWnd* pParentWnd, UINT nID, 
						CCreateContext* pContext = NULL)
	{ 
		return CreateControl(GetClsid(), lpszWindowName, dwStyle, rect, pParentWnd, nID); 
	}

    BOOL Create(LPCTSTR lpszWindowName, DWORD dwStyle, const RECT& rect, CWnd* pParentWnd, 
				UINT nID, CFile* pPersist = NULL, BOOL bStorage = FALSE,
				BSTR bstrLicKey = NULL)
	{ 
		return CreateControl(GetClsid(), lpszWindowName, dwStyle, rect, pParentWnd, nID,
		pPersist, bStorage, bstrLicKey); 
	}

// Attributes
public:

// Operations
public:

	void AddAttribute(LPCTSTR key, LPCTSTR value)
	{
		static BYTE parms[] = VTS_BSTR VTS_BSTR ;
		InvokeHelper(0x1, DISPATCH_METHOD, VT_EMPTY, NULL, parms, key, value);
	}
	void RemoveAttribute(LPCTSTR key)
	{
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x2, DISPATCH_METHOD, VT_EMPTY, NULL, parms, key);
	}
	CString GetAttribute(LPCTSTR key)
	{
		CString result;
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x3, DISPATCH_METHOD, VT_BSTR, (void*)&result, parms, key);
		return result;
	}
	CString Navigate()
	{
		CString result;
		InvokeHelper(0x4, DISPATCH_METHOD, VT_BSTR, (void*)&result, NULL);
		return result;
	}


};
