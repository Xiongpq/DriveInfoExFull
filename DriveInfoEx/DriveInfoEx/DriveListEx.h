#pragma once
using namespace System;
using namespace System::Runtime::InteropServices;
using namespace System::Collections::Generic;

namespace IOEx {
	public ref class DriveListEx : System::Collections::Generic::List<DriveInfoEx ^>
	{
		UINT m_count = 0;
	public:

		DriveListEx(void)
		{
				DiskInfo& di = DiskInfo::GetDiskInfo();
				di.LoadDiskInfo();
				m_count = di.m_DriveCount;
				for(UINT i=0; i < m_count; i++)
					this->Add<DriveInfoEx^>(new DriveInfoEx(i));
				//String^ ms = di.GetSerialNumber(0);
				//ms =di.GetDriveType(0);
				//ms = di.GetModelNumber(0);
				//ms = di.GetRevisionNumber(0);
				//unsigned __int64 size = di.DriveSize(0);
				//size = di.BufferSize(0);
		}

		virtual ~DriveListEx(void)
		{
		}
	};
}
