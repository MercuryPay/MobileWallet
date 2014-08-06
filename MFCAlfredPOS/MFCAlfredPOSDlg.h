
// MFCAlfredPOSDlg.h : header file
//

#pragma once
#include "afxwin.h"
#include "CMpsWidgetControl.h"

// CMFCAlfredPOSDlg dialog
class CMFCAlfredPOSDlg : public CDialogEx
{
// Construction
public:
	CMFCAlfredPOSDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_MFCALFREDPOS_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:

	
	//CMpsWidgetControl m_MobileWalletCtrl;
	DECLARE_EVENTSINK_MAP()
	void DataReadyMpswidgetcontrol1(LPCTSTR data);
	CMpsWidgetControl m_MobileWalletCtrl;
};
