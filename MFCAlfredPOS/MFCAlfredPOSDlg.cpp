
// MFCAlfredPOSDlg.cpp : implementation file
//

#include "stdafx.h"
#include "MFCAlfredPOS.h"
#include "MFCAlfredPOSDlg.h"
#include "afxdialogex.h"


#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CAboutDlg dialog used for App About

class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

// Dialog Data
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

// Implementation
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialogEx(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)
END_MESSAGE_MAP()


// CMFCAlfredPOSDlg dialog



CMFCAlfredPOSDlg::CMFCAlfredPOSDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CMFCAlfredPOSDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CMFCAlfredPOSDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);

	DDX_Control(pDX, IDC_MPSWIDGETCONTROL1, m_MobileWalletCtrl);
}

BEGIN_MESSAGE_MAP(CMFCAlfredPOSDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
END_MESSAGE_MAP()


// CMFCAlfredPOSDlg message handlers

BOOL CMFCAlfredPOSDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here
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

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CMFCAlfredPOSDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialogEx::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CMFCAlfredPOSDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CMFCAlfredPOSDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
	///////
}

BEGIN_EVENTSINK_MAP(CMFCAlfredPOSDlg, CDialogEx)
	ON_EVENT(CMFCAlfredPOSDlg, IDC_MPSWIDGETCONTROL1, 1, CMFCAlfredPOSDlg::DataReadyMpswidgetcontrol1, VTS_BSTR)
END_EVENTSINK_MAP()


void CMFCAlfredPOSDlg::DataReadyMpswidgetcontrol1(LPCTSTR data)
{
	// TODO: Add your message handler code here
	AfxMessageBox(data);
}
