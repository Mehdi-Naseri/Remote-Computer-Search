using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Management;


namespace RemoteComputerSystemInformation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            //Fill Combobox
            combobox1.Items.Add("Customized");
            foreach (string stringWin32class in stringWin32classes)
            {
                combobox1.Items.Add(stringWin32class);
            }
            combobox1.SelectedIndex = 0;
            textBoxQueryString.AcceptsReturn = true;
            textBoxFileSize.Text = "100000000";
            textBoxFileName.Text = "pass";
            textBoxDrive.MaxLength = 1;

            //textBoxQueryString.Text = textBoxQueryString.Text = "Select * from CIM_DataFile where (FileSize > 1000 Or FileName Like \"%pass%\") And Drive = \"I:\"";

            //textboxIP.Text = "172.16.89.32";
            //textboxUsername.Text = "PZ\\120723";
            //passwordbox1.Password = "1234abcd!";

            textboxIP.Text = "172.16.255.35";
            textboxUsername.Text = "172.16.255.35\\administrator";
            passwordbox1.Password = "#1Amgid608";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (combobox1.SelectedItem.ToString() == "Customized")
            {
                // ServerInfoCustomized(strUsername, strPassword, strIP, ListBoxIn);
                RemoteComputerInfoCustomized(textboxUsername.Text, passwordbox1.Password, textboxIP.Text, listboxResult);
            }
            else
            {
                RemoteComputerInfo(textboxUsername.Text, passwordbox1.Password, textboxIP.Text, listboxResult, combobox1.Text);
            }
        }

        #region Remote Computer System Information
        private void RemoteComputerInfoCustomized(string strUsername, string strPassword, string strIP, ListBox ListBoxIn)
        {
            ListBox ListBoxResult = ListBoxIn;
            ListBoxResult.Items.Clear();

            ConnectionOptions options = new ConnectionOptions();
            options.Username = strUsername;
            options.Password = strPassword;
            options.Impersonation = ImpersonationLevel.Impersonate;
            options.EnablePrivileges = true;
            try
            {
                ManagementScope ManagementScope1 = new ManagementScope(string.Format("\\\\{0}\\root\\cimv2", strIP), options);
                ManagementScope1.Connect();
                ObjectGetOptions objectGetOptions = new ObjectGetOptions();
                ManagementPath managementPath1 = new ManagementPath("Win32_OperatingSystem");
                ManagementClass ManagementClass1 = new ManagementClass(ManagementScope1, managementPath1, objectGetOptions);

                foreach (ManagementObject ManagementObject1 in ManagementClass1.GetInstances())
                {
                    // Display the remote computer information
                    ListBoxResult.Items.Add(string.Format("Computer Name : {0}", ManagementObject1["csname"]));
                    ListBoxResult.Items.Add(string.Format("Windows Directory : {0}", ManagementObject1["WindowsDirectory"]));
                    ListBoxResult.Items.Add(string.Format("Operating System: {0}", ManagementObject1["Caption"]));
                    ListBoxResult.Items.Add(string.Format("Version: {0}", ManagementObject1["Version"]));
                    ListBoxResult.Items.Add(string.Format("Manufacturer : {0}", ManagementObject1["Manufacturer"]));
                    ListBoxResult.Items.Add(string.Format("Latest bootup time : {0}", ManagementObject1["LastBootUpTime"]));
                }
            }
            catch (Exception ex)
            {
                ListBoxResult.Items.Add(string.Format("Can't Connect to Server: {0}\n{1}", strIP, ex.Message));
            }
        }

        private void RemoteComputerInfo(string strUsername, string strPassword, string strIP, ListBox ListBoxIn, string stringWin32class)
        {
            ListBox ListBoxResult = ListBoxIn;
            ListBoxResult.Items.Clear();

            ConnectionOptions options = new ConnectionOptions();
            options.Username = strUsername;
            options.Password = strPassword;
            options.Impersonation = ImpersonationLevel.Impersonate;
            options.EnablePrivileges = true;
            try
            {
                ManagementScope ManagementScope1 = new ManagementScope(string.Format("\\\\{0}\\root\\cimv2", strIP), options);
                ManagementScope1.Connect();

                ObjectGetOptions objectGetOptions = new ObjectGetOptions();
                ManagementPath managementPath1 = new ManagementPath(stringWin32class);
                ManagementClass ManagementClass1 = new ManagementClass(ManagementScope1, managementPath1, objectGetOptions);

                PropertyDataCollection PropertyDataCollection1 = ManagementClass1.Properties;

                foreach (ManagementObject ManagementObject1 in ManagementClass1.GetInstances())
                {
                    foreach (PropertyData property in PropertyDataCollection1)
                    {
                        try
                        {
                            // Display the remote computer system information in input listbox
                            ListBoxResult.Items.Add(string.Format(property.Name + ":  " + ManagementObject1.Properties[property.Name].Value.ToString()));
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ListBoxResult.Items.Add(string.Format("Can't Connect to Server: {0}\n{1}", strIP, ex.Message));
            }
        }
        #endregion

        #region stringWin32classes
        string[] stringWin32classes = {"Win32_1394Controller",
"Win32_1394ControllerDevice",
"Win32_AccountSID",
"Win32_ActionCheck",
"Win32_ActiveRoute",
"Win32_AllocatedResource",
"Win32_ApplicationCommandLine",
"Win32_ApplicationService",
"Win32_AssociatedBattery",
"Win32_AssociatedProcessorMemory",
"Win32_AutochkSetting",
"Win32_BaseBoard",
"Win32_Battery",
"Win32_Binary",
"Win32_BindImageAction",
"Win32_BIOS",
"Win32_BootConfiguration",
"Win32_Bus"+
"Win32_CacheMemory",
"Win32_CDROMDrive",
"Win32_CheckCheck",
"Win32_CIMLogicalDeviceCIMDataFile",
"Win32_ClassicCOMApplicationClasses",
"Win32_ClassicCOMClass",
"Win32_ClassicCOMClassSetting",
"Win32_ClassicCOMClassSettings",
"Win32_ClassInforAction",
"Win32_ClientApplicationSetting",
"Win32_CodecFile",
"Win32_COMApplicationSettings",
"Win32_COMClassAutoEmulator",
"Win32_ComClassEmulator",
"Win32_CommandLineAccess",
"Win32_ComponentCategory",
"Win32_ComputerSystem",
"Win32_ComputerSystemProcessor",
"Win32_ComputerSystemProduct",
"Win32_ComputerSystemWindowsProductActivationSetting",
"Win32_Condition",
"Win32_ConnectionShare",
"Win32_ControllerHastHub",
"Win32_CreateFolderAction",
"Win32_CurrentProbe",
"Win32_DCOMApplication",
"Win32_DCOMApplicationAccessAllowedSetting",
"Win32_DCOMApplicationLaunchAllowedSetting",
"Win32_DCOMApplicationSetting",
"Win32_DependentService",
"Win32_Desktop",
"Win32_DesktopMonitor",
"Win32_DeviceBus",
"Win32_DeviceMemoryAddress",
"Win32_Directory",
"Win32_DirectorySpecification",
"Win32_DiskDrive",
"Win32_DiskDrivePhysicalMedia",
"Win32_DiskDriveToDiskPartition",
"Win32_DiskPartition",
"Win32_DiskQuota",
"Win32_DisplayConfiguration",
"Win32_DisplayControllerConfiguration",
"Win32_DMAChanner",
"Win32_DriverForDevice",
"Win32_DriverVXD",
"Win32_DuplicateFileAction",
"Win32_Environment",
"Win32_EnvironmentSpecification",
"Win32_ExtensionInfoAction",
"Win32_Fan",
"Win32_FileSpecification",
"Win32_FloppyController",
"Win32_FloppyDrive",
"Win32_FontInfoAction",
"Win32_Group",
"Win32_GroupDomain",
"Win32_GroupUser",
"Win32_HeatPipe",
"Win32_IDEController",
"Win32_IDEControllerDevice",
"Win32_ImplementedCategory",
"Win32_InfraredDevice",
"Win32_IniFileSpecification",
"Win32_InstalledSoftwareElement",
"Win32_IP4PersistedRouteTable",
"Win32_IP4RouteTable",
"Win32_IRQResource",
"Win32_Keyboard",
"Win32_LaunchCondition",
"Win32_LoadOrderGroup",
"Win32_LoadOrderGroupServiceDependencies",
"Win32_LoadOrderGroupServiceMembers",
"Win32_LocalTime",
"Win32_LoggedOnUser",
"Win32_LogicalDisk",
"Win32_LogicalDiskRootDirectory",
"Win32_LogicalDiskToPartition",
"Win32_LogicalFileAccess",
"Win32_LogicalFileAuditing",
"Win32_LogicalFileGroup",
"Win32_LogicalFileOwner",
"Win32_LogicalFileSecuritySetting",
"Win32_LogicalMemoryConfiguration",
"Win32_LogicalProgramGroup",
"Win32_LogicalProgramGroupDirectory",
"Win32_LogicalProgramGroupItem",
"Win32_LogicalProgramGroupItemDataFile",
"Win32_LogicalShareAccess",
"Win32_LogicalShareAuditing",
"Win32_LogicalShareSecuritySetting",
"Win32_LogonSession",
"Win32_LogonSessionMappedDisk",
"Win32_MappedLogicalDisk",
"Win32_MemoryArray",
"Win32_MemoryArrayLocation",
"Win32_MemoryDevice",
"Win32_MemoryDeviceArray",
"Win32_MemoryDeviceLocation",
"Win32_MIMEInfoAction",
"Win32_MotherboardDevice",
"Win32_MoveFileAction",
"Win32_NamedJobObject",
"Win32_NamedJobObjectActgInfo",
"Win32_NamedJobObjectLimit",
"Win32_NamedJobObjectLimitSetting",
"Win32_NamedJobObjectProcess",
"Win32_NamedJobObjectSecLimit",
"Win32_NamedJobObjectSecLimitSetting",
"Win32_NamedJobObjectStatistics",
"Win32_NetworkAdapter",
"Win32_NetworkAdapterConfiguration",
"Win32_NetworkAdapterSetting",
"Win32_NetworkClient",
"Win32_NetworkConnection",
"Win32_NetworkLoginProfile",
"Win32_NetworkProtocol",
"Win32_NTDomain",
"Win32_NTEventlogFile",
"Win32_NTLogEvent",
"Win32_NTLogEventComputer",
"Win32_NTLogEvnetLog",
"Win32_NTLogEventUser",
"Win32_ODBCAttribute",
"Win32_ODBCDataSourceAttribute",
"Win32_ODBCDataSourceSpecification",
"Win32_ODBCDriverAttribute",
"Win32_ODBCDriverSoftwareElement",
"Win32_ODBCDriverSpecification",
"Win32_ODBCSourceAttribute",
"Win32_ODBCTranslatorSpecification",
"Win32_OnBoardDevice",
"Win32_OperatingSystem",
"Win32_OperatingSystemAutochkSetting",
"Win32_OperatingSystemQFE",
"Win32_OSRecoveryConfiguration",
"Win32_PageFile",
"Win32_PageFileElementSetting",
"Win32_PageFileSetting",
"Win32_PageFileUsage",
"Win32_ParallelPort",
"Win32_Patch",
"Win32_PatchFile",
"Win32_PatchPackage",
"Win32_PCMCIAControler",
"Win32_PerfFormattedData_ASP_ActiveServerPages",
"Win32_PerfFormattedData_ASPNET_114322_ASPNETAppsv114322",
"Win32_PerfFormattedData_ASPNET_114322_ASPNETv114322",
"Win32_PerfFormattedData_ASPNET_2040607_ASPNETAppsv2040607",
"Win32_PerfFormattedData_ASPNET_2040607_ASPNETv2040607",
"Win32_PerfFormattedData_ASPNET_ASPNET",
"Win32_PerfFormattedData_ASPNET_ASPNETApplications",
"Win32_PerfFormattedData_aspnet_state_ASPNETStateService",
"Win32_PerfFormattedData_ContentFilter_IndexingServiceFilter",
"Win32_PerfFormattedData_ContentIndex_IndexingService",
"Win32_PerfFormattedData_DTSPipeline_SQLServerDTSPipeline",
"Win32_PerfFormattedData_Fax_FaxServices",
"Win32_PerfFormattedData_InetInfo_InternetInformationServicesGlobal",
"Win32_PerfFormattedData_ISAPISearch_HttpIndexingService",
"Win32_PerfFormattedData_MSDTC_DistributedTransactionCoordinator",
"Win32_PerfFormattedData_NETCLRData_NETCLRData",
"Win32_PerfFormattedData_NETCLRNetworking_NETCLRNetworking",
"Win32_PerfFormattedData_NETDataProviderforOracle_NETCLRData",
"Win32_PerfFormattedData_NETDataProviderforSqlServer_NETDataProviderforSqlServer",
"Win32_PerfFormattedData_NETFramework_NETCLRExceptions",
"Win32_PerfFormattedData_NETFramework_NETCLRInterop",
"Win32_PerfFormattedData_NETFramework_NETCLRJit",
"Win32_PerfFormattedData_NETFramework_NETCLRLoading",
"Win32_PerfFormattedData_NETFramework_NETCLRLocksAndThreads",
"Win32_PerfFormattedData_NETFramework_NETCLRMemory",
"Win32_PerfFormattedData_NETFramework_NETCLRRemoting",
"Win32_PerfFormattedData_NETFramework_NETCLRSecurity",
"Win32_PerfFormattedData_NTFSDRV_ControladordealmacenamientoNTFSdeSMTP",
"Win32_PerfFormattedData_Outlook_Outlook",
"Win32_PerfFormattedData_PerfDisk_LogicalDisk",
"Win32_PerfFormattedData_PerfDisk_PhysicalDisk",
"Win32_PerfFormattedData_PerfNet_Browser",
"Win32_PerfFormattedData_PerfNet_Redirector",
"Win32_PerfFormattedData_PerfNet_Server",
"Win32_PerfFormattedData_PerfNet_ServerWorkQueues",
"Win32_PerfFormattedData_PerfOS_Cache",
"Win32_PerfFormattedData_PerfOS_Memory",
"Win32_PerfFormattedData_PerfOS_Objects",
"Win32_PerfFormattedData_PerfOS_PagingFile",
"Win32_PerfFormattedData_PerfOS_Processor",
"Win32_PerfFormattedData_PerfOS_System",
"Win32_PerfFormattedData_PerfProc_FullImage_Costly",
"Win32_PerfFormattedData_PerfProc_Image_Costly",
"Win32_PerfFormattedData_PerfProc_JobObject",
"Win32_PerfFormattedData_PerfProc_JobObjectDetails",
"Win32_PerfFormattedData_PerfProc_Process",
"Win32_PerfFormattedData_PerfProc_ProcessAddressSpace_Costly",
"Win32_PerfFormattedData_PerfProc_Thread",
"Win32_PerfFormattedData_PerfProc_ThreadDetails_Costly",
"Win32_PerfFormattedData_RemoteAccess_RASPort",
"Win32_PerfFormattedData_RemoteAccess_RASTotal",
"Win32_PerfFormattedData_RSVP_RSVPInterfaces",
"Win32_PerfFormattedData_RSVP_RSVPService",
"Win32_PerfFormattedData_Spooler_PrintQueue",
"Win32_PerfFormattedData_TapiSrv_Telephony",
"Win32_PerfFormattedData_Tcpip_ICMP",
"Win32_PerfFormattedData_Tcpip_IP",
"Win32_PerfFormattedData_Tcpip_NBTConnection",
"Win32_PerfFormattedData_Tcpip_NetworkInterface",
"Win32_PerfFormattedData_Tcpip_TCP",
"Win32_PerfFormattedData_Tcpip_UDP",
"Win32_PerfFormattedData_TermService_TerminalServices",
"Win32_PerfFormattedData_TermService_TerminalServicesSession",
"Win32_PerfFormattedData_W3SVC_WebService",
"Win32_PerfRawData_ASP_ActiveServerPages",
"Win32_PerfRawData_ASPNET_114322_ASPNETAppsv114322",
"Win32_PerfRawData_ASPNET_114322_ASPNETv114322",
"Win32_PerfRawData_ASPNET_2040607_ASPNETAppsv2040607",
"Win32_PerfRawData_ASPNET_2040607_ASPNETv2040607",
"Win32_PerfRawData_ASPNET_ASPNET",
"Win32_PerfRawData_ASPNET_ASPNETApplications",
"Win32_PerfRawData_aspnet_state_ASPNETStateService",
"Win32_PerfRawData_ContentFilter_IndexingServiceFilter",
"Win32_PerfRawData_ContentIndex_IndexingService",
"Win32_PerfRawData_DTSPipeline_SQLServerDTSPipeline",
"Win32_PerfRawData_Fax_FaxServices",
"Win32_PerfRawData_InetInfo_InternetInformationServicesGlobal",
"Win32_PerfRawData_ISAPISearch_HttpIndexingService",
"Win32_PerfRawData_MSDTC_DistributedTransactionCoordinator",
"Win32_PerfRawData_NETCLRData_NETCLRData",
"Win32_PerfRawData_NETCLRNetworking_NETCLRNetworking",
"Win32_PerfRawData_NETDataProviderforOracle_NETCLRData",
"Win32_PerfRawData_NETDataProviderforSqlServer_NETDataProviderforSqlServer",
"Win32_PerfRawData_NETFramework_NETCLRExceptions",
"Win32_PerfRawData_NETFramework_NETCLRInterop",
"Win32_PerfRawData_NETFramework_NETCLRJit",
"Win32_PerfRawData_NETFramework_NETCLRLoading",
"Win32_PerfRawData_NETFramework_NETCLRLocksAndThreads",
"Win32_PerfRawData_NETFramework_NETCLRMemory",
"Win32_PerfRawData_NETFramework_NETCLRRemoting",
"Win32_PerfRawData_NETFramework_NETCLRSecurity",
"Win32_PerfRawData_NTFSDRV_ControladordealmacenamientoNTFSdeSMTP",
"Win32_PerfRawData_Outlook_Outlook",
"Win32_PerfRawData_PerfDisk_LogicalDisk",
"Win32_PerfRawData_PerfDisk_PhysicalDisk",
"Win32_PerfRawData_PerfNet_Browser",
"Win32_PerfRawData_PerfNet_Redirector",
"Win32_PerfRawData_PerfNet_Server",
"Win32_PerfRawData_PerfNet_ServerWorkQueues",
"Win32_PerfRawData_PerfOS_Cache",
"Win32_PerfRawData_PerfOS_Memory",
"Win32_PerfRawData_PerfOS_Objects",
"Win32_PerfRawData_PerfOS_PagingFile",
"Win32_PerfRawData_PerfOS_Processor",
"Win32_PerfRawData_PerfOS_System",
"Win32_PerfRawData_PerfProc_FullImage_Costly",
"Win32_PerfRawData_PerfProc_Image_Costly",
"Win32_PerfRawData_PerfProc_JobObject",
"Win32_PerfRawData_PerfProc_JobObjectDetails",
"Win32_PerfRawData_PerfProc_Process",
"Win32_PerfRawData_PerfProc_ProcessAddressSpace_Costly",
"Win32_PerfRawData_PerfProc_Thread",
"Win32_PerfRawData_PerfProc_ThreadDetails_Costly",
"Win32_PerfRawData_RemoteAccess_RASPort",
"Win32_PerfRawData_RemoteAccess_RASTotal",
"Win32_PerfRawData_RSVP_RSVPInterfaces",
"Win32_PerfRawData_RSVP_RSVPService",
"Win32_PerfRawData_Spooler_PrintQueue",
"Win32_PerfRawData_TapiSrv_Telephony",
"Win32_PerfRawData_Tcpip_ICMP",
"Win32_PerfRawData_Tcpip_IP",
"Win32_PerfRawData_Tcpip_NBTConnection",
"Win32_PerfRawData_Tcpip_NetworkInterface",
"Win32_PerfRawData_Tcpip_TCP",
"Win32_PerfRawData_Tcpip_UDP",
"Win32_PerfRawData_TermService_TerminalServices",
"Win32_PerfRawData_TermService_TerminalServicesSession",
"Win32_PerfRawData_W3SVC_WebService",
"Win32_PhysicalMedia",
"Win32_PhysicalMemory",
"Win32_PhysicalMemoryArray",
"Win32_PhysicalMemoryLocation",
"Win32_PingStatus",
"Win32_PNPAllocatedResource",
"Win32_PnPDevice",
"Win32_PnPEntity",
"Win32_PnPSignedDriver",
"Win32_PnPSignedDriverCIMDataFile",
"Win32_PointingDevice",
"Win32_PortableBattery",
"Win32_PortConnector",
"Win32_PortResource",
"Win32_POTSModem",
"Win32_POTSModemToSerialPort",
"Win32_Printer",
"Win32_PrinterConfiguration",
"Win32_PrinterController",
"Win32_PrinterDriver",
"Win32_PrinterDriverDll",
"Win32_PrinterSetting",
"Win32_PrinterShare",
"Win32_PrintJob",
"Win32_Process",
"Win32_Processor",
"Win32_Product",
"Win32_ProductCheck",
"Win32_ProductResource",
"Win32_ProductSoftwareFeatures",
"Win32_ProgIDSpecification",
"Win32_ProgramGroup",
"Win32_ProgramGroupContents",
"Win32_Property",
"Win32_ProtocolBinding",
"Win32_Proxy",
"Win32_PublishComponentAction",
"Win32_QuickFixEngineering",
"Win32_QuotaSetting",
"Win32_Refrigeration",
"Win32_Registry",
"Win32_RegistryAction",
"Win32_RemoveFileAction",
"Win32_RemoveIniAction",
"Win32_ReserveCost",
"Win32_ScheduledJob",
"Win32_SCSIController",
"Win32_SCSIControllerDevice",
"Win32_SecuritySettingOfLogicalFile",
"Win32_SecuritySettingOfLogicalShare",
"Win32_SelfRegModuleAction",
"Win32_SerialPort",
"Win32_SerialPortConfiguration",
"Win32_SerialPortSetting",
"Win32_ServerConnection",
"Win32_ServerSession",
"Win32_Service",
"Win32_ServiceControl",
"Win32_ServiceSpecification",
"Win32_ServiceSpecificationService",
"Win32_SessionConnection",
"Win32_SessionProcess",
"Win32_Share",
"Win32_ShareToDirectory",
"Win32_ShortcutAction",
"Win32_ShortcutFile",
"Win32_ShortcutSAP",
"Win32_SID",
"Win32_SoftwareElement",
"Win32_SoftwareElementAction",
"Win32_SoftwareElementCheck",
"Win32_SoftwareElementCondition",
"Win32_SoftwareElementResource",
"Win32_SoftwareFeature",
"Win32_SoftwareFeatureAction",
"Win32_SoftwareFeatureCheck",
"Win32_SoftwareFeatureParent",
"Win32_SoftwareFeatureSoftwareElements",
"Win32_SoundDevice",
"Win32_StartupCommand",
"Win32_SubDirectory",
"Win32_SystemAccount",
"Win32_SystemBIOS",
"Win32_SystemBootConfiguration",
"Win32_SystemDesktop",
"Win32_SystemDevices",
"Win32_SystemDriver",
"Win32_SystemDriverPNPEntity",
"Win32_SystemEnclosure",
"Win32_SystemLoadOrderGroups",
"Win32_SystemLogicalMemoryConfiguration",
"Win32_SystemNetworkConnections",
"Win32_SystemOperatingSystem",
"Win32_SystemPartitions",
"Win32_SystemProcesses",
"Win32_SystemProgramGroups",
"Win32_SystemResources",
"Win32_SystemServices",
"Win32_SystemSlot",
"Win32_SystemSystemDriver",
"Win32_SystemTimeZone",
"Win32_SystemUsers",
"Win32_TapeDrive",
"Win32_TCPIPPrinterPort",
"Win32_TemperatureProbe",
"Win32_Terminal",
"Win32_TerminalService",
"Win32_TerminalServiceSetting",
"Win32_TerminalServiceToSetting",
"Win32_TerminalTerminalSetting",
"Win32_Thread",
"Win32_TimeZone",
"Win32_TSAccount",
"Win32_TSClientSetting",
"Win32_TSEnvironmentSetting",
"Win32_TSGeneralSetting",
"Win32_TSLogonSetting",
"Win32_TSNetworkAdapterListSetting",
"Win32_TSNetworkAdapterSetting",
"Win32_TSPermissionsSetting",
"Win32_TSRemoteControlSetting",
"Win32_TSSessionDirectory",
"Win32_TSSessionDirectorySetting",
"Win32_TSSessionSetting",
"Win32_TypeLibraryAction",
"Win32_UninterruptiblePowerSupply",
"Win32_USBController",
"Win32_USBControllerDevice",
"Win32_USBHub",
"Win32_UserAccount",
"Win32_UserDesktop",
"Win32_UserInDomain",
"Win32_UTCTime",
"Win32_VideoController",
"Win32_VideoSettings",
"Win32_VoltageProbe",
"Win32_VolumeQuotaSetting",
"Win32_WindowsProductActivation",
"Win32_WMIElementSetting",
"Win32_WMISetting",
"CIM_DataFile"
};
        #endregion

        private void buttonFindFiles_Click(object sender, RoutedEventArgs e)
        {
            SearchFiles(StringIP: textboxIP.Text, StringUsername: textboxUsername.Text, StringPassword: passwordbox1.Password,
                DatadridResult: DataGridFiles, stringQueryString: textBoxQueryString.Text);
        }

        public void SearchFiles(string StringIP, string StringUsername, String StringPassword,DataGrid DatadridResult , string stringQueryString)
        {
            ConnectionOptions options = new ConnectionOptions();
            options.Username = StringUsername;
            options.Password = StringPassword;
            options.Impersonation = ImpersonationLevel.Impersonate;
            options.EnablePrivileges = true;
            try
            {
                //Connect to remote Computer
                ManagementScope ManagementScope1 = new ManagementScope(string.Format("\\\\{0}\\root\\cimv2", StringIP), options);
                ManagementScope1.Connect();
                ManagementPath managementPath1 = new ManagementPath("CIM_DataFile");

                //Filter Output Data
                SelectQuery SelectQuery1 = new SelectQuery();
                SelectQuery1.QueryString = stringQueryString;
                ManagementObjectSearcher ManagementObjectSearcher1 = new ManagementObjectSearcher(ManagementScope1, SelectQuery1);

                //MessageBox.Show(ManagementObjectSearcher1.Get().Count.ToString());
                ClassCIM_DataFiles1.Clear();
                foreach (ManagementObject ManagementObject1 in ManagementObjectSearcher1.Get())
                {
                    try
                    {
                        ClassCIM_DataFile ClassCIM_DataFile1 = new MainWindow.ClassCIM_DataFile();

                        ClassCIM_DataFile1.FileName = ManagementObject1.Properties["FileName"].Value.ToString();
                        ClassCIM_DataFile1.Extension = ManagementObject1.Properties["Extension"].Value.ToString();
                        ClassCIM_DataFile1.FileSize = UInt64.Parse(ManagementObject1.Properties["FileSize"].Value.ToString());
                        ClassCIM_DataFile1.FileType = ManagementObject1.Properties["FileType"].Value.ToString();
                        ClassCIM_DataFile1.Name = ManagementObject1.Properties["Name"].Value.ToString();
                        ClassCIM_DataFile1.Hidden = (Boolean)ManagementObject1.Properties["Hidden"].Value;
                        //ClassCIM_DataFile1.CreationDate = DateTime.Parse(ManagementObject1.Properties["CreationDate"].Value.ToString());
                        //ClassCIM_DataFile1.LastAccessed = (DateTime)ManagementObject1.Properties["LastAccessed"].Value;
                        //ClassCIM_DataFile1.LastModified = (DateTime)ManagementObject1.Properties["LastModified"].Value;

                        ClassCIM_DataFiles1.Add(ClassCIM_DataFile1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("Can't show data: {0}\n{1}", StringIP, ex.Message));
                    }
                }
                DataGridFiles.ItemsSource = ClassCIM_DataFiles1;
                DataGridFiles.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Can't recieve files from server: {0}\n{1}", StringIP, ex.Message));
            }
        }

        private void ButtonSaveFileDescription_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.SaveFileDialog SaveFileDialog1 = new Microsoft.Win32.SaveFileDialog();
                SaveFileDialog1.Filter = "Excel Workbook (*.csv)|*.csv";
                if ((bool)SaveFileDialog1.ShowDialog())
                {
                    Export2CSV(SaveFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Export2CSV(string stringFileName)
        {
            StringBuilder StringBuilder1 = new StringBuilder(null);
            foreach (string string1 in ClassCIM_DataFile.StringArrayProperties)
            {
                if (StringBuilder1.Length == 0)
                    StringBuilder1.Append(string1);
                else
                    StringBuilder1.Append(',' + string1);
            }
            StringBuilder1.AppendLine();
            foreach (ClassCIM_DataFile ClassCIM_DataFile1 in ClassCIM_DataFiles1)
            {
                StringBuilder StringBuilderTemp = new StringBuilder(null);
                foreach (string string1 in ClassCIM_DataFile1.Properties())
                {
                    if (StringBuilderTemp.Length == 0)
                        StringBuilderTemp.Append(string1);
                    else
                        StringBuilderTemp.Append(',' + string1);
                }
                //   StringBuilder1.AppendLine();
                StringBuilder1.AppendLine(StringBuilderTemp.ToString());
            }
            System.IO.File.WriteAllText(stringFileName, StringBuilder1.ToString(), Encoding.UTF8);
            MessageBox.Show("Saved Successfully", "Save Results", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #region  ClassCIM_DataFile
        public ClassCIM_DataFiles ClassCIM_DataFiles1 = new ClassCIM_DataFiles();
        public class ClassCIM_DataFiles : List<ClassCIM_DataFile> { }
        public class ClassCIM_DataFile
        {
            public string FileName { get; set; }
            public string Extension { get; set; }
            public UInt64 FileSize { get; set; }
            public string FileType { get; set; }
            public string Name { get; set; }
            public bool? Hidden { get; set; }
            public DateTime? LastModified { get; set; }
            public DateTime? LastAccessed { get; set; }
            public DateTime? CreationDate { get; set; }
            public ClassCIM_DataFile() { }
            public ClassCIM_DataFile(DateTime CreationDate, string Drive, string Extension, string FileName, UInt64 FileSize, string FileType, bool Hidden,
                                     DateTime LastAccessed, DateTime LastModified, string Name, string Path)
            {
                this.FileName = FileName;
                this.Extension = Extension;
                this.FileSize = FileSize;
                this.FileType = FileType;
                this.Name = Name;
                this.Hidden = Hidden;
                this.LastModified = LastModified;
                this.LastAccessed = LastAccessed;
                this.CreationDate = CreationDate;
            }
            public List<string> Properties()
            {
                return new List<string> {FileName,Extension, FileSize.ToString(),FileType,Name,Hidden.ToString(),
                                         LastModified.ToString(),LastAccessed.ToString(),CreationDate.ToString()};
            }
            public int UserPropertiesTotal = 9;
            public static string[] StringArrayProperties = { "FileName", "Extension","FileSize","FileType","Name","Hidden",
                                                               "LastModified","LastAccessed","CreationDate"  };
        };
        #endregion


#region CIM_DataFile Original Class 
//class CIM_DataFile : CIM_LogicalFile
//{
//    uint32 AccessMask;
//    boolean Archive;
//    string Caption;
//    boolean Compressed;
//    string CompressionMethod;
//    string CreationClassName;
//    datetime CreationDate;
//    string CSCreationClassName;
//    string CSName;
//    string Description;
//    string Drive;
//    string EightDotThreeFileName;
//    boolean Encrypted;
//    string EncryptionMethod;
//    string Extension;
//    string FileName;
//    uint64 FileSize;
//    string FileType;
//    string FSCreationClassName;
//    string FSName;
//    boolean Hidden;
//    datetime InstallDate;
//    uint64 InUseCount;
//    datetime LastAccessed;
//    datetime LastModified;
//    string Manufacturer;
//    string Name;
//    string Path;
//    boolean Readable;
//    string Status;
//    boolean System;
//    string Version;
//    boolean Writeable;
//};
#endregion

        #region Manage Interface
        private void textBoxFileSize_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex("[^0-9]");
            e.Handled = regex1.IsMatch(e.Text);
        }
        #endregion

        private void textBoxFileProperties_TextChanged(object sender, TextChangedEventArgs e)
        {
            MakeWMIQuery();
        }

        public void MakeWMIQuery()
        {
            //textBoxQueryString.Text = string.Format("Select * from CIM_DataFile\n where (FileSize > {0} and FileName Like \"{1}\" And Drive = \"I:\")", textBoxFileSize.Text, textBoxFileName.Text);
            StringBuilder StringBuilder1 = new StringBuilder("Select * from CIM_DataFile\n ");
            if (textBoxDrive.Text.Length > 0 || textBoxExtension.Text.Length > 0 || textBoxFileName.Text.Length > 0 || textBoxFileSize.Text.Length > 0)
            {
                StringBuilder1.Append("where (");
                if (textBoxDrive.Text != null)
                    StringBuilder1.AppendFormat("Drive = \"{0}:\" ", textBoxDrive.Text);
                if (textBoxFileSize.Text != null)
                    StringBuilder1.AppendFormat("FileSize = {0} ", textBoxFileSize.Text);
                if (textBoxExtension.Text.Length > 0)
                    StringBuilder1.AppendFormat("Extension = \"{0}\" ", textBoxExtension.Text);
                if (textBoxFileName.Text.Length > 0)
                    StringBuilder1.AppendFormat("FileName Like \"%{0}%\" ", textBoxFileName.Text);
                StringBuilder1.Append(")");
            }
            textBoxQueryString.Text = StringBuilder1.ToString();

        }
            

    }
}


