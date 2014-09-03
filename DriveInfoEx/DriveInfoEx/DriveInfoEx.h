// DriveInfoEx.h

#pragma once
#include "unmanagedcode.h"

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace System::Collections::Generic;

namespace IOEx {
	public ref class DriveInfoEx
	{
		unsigned __int64 m_DriveSize,m_BufferSize;
	public:
		DriveInfoEx(UINT cnt)
		{
			DiskInfo& di = DiskInfo::GetDiskInfo();
			//di.LoadDiskInfo();
			SerialNumber = di.GetSerialNumber(cnt);
			DriveType =di.GetDriveType(cnt);
			ModelNumber = di.GetModelNumber(cnt);
			RevisionNumber = di.GetRevisionNumber(cnt);
			m_DriveSize = di.DriveSize(cnt);
			m_BufferSize = di.BufferSize(cnt);
		}

		DriveInfoEx(char* serialNum)
		{
			DiskInfo& di = DiskInfo::GetDiskInfo();
			SerialNumber = di.CGetString(serialNum,0,19)->Trim();
		}

		!DriveInfoEx()
		{
		}
		virtual ~DriveInfoEx()
		{
			this->!DriveInfoEx();
		}

		   // property data member
		   property String ^ SerialNumber;
		   property String ^ DriveType;
		   property String ^ ModelNumber;
		   property String ^ RevisionNumber;
		   // property block
		   property unsigned __int64 BufferSize {

			   unsigned __int64 get(){return m_BufferSize;};

		   }
		   property unsigned __int64 DriveSize {

			   unsigned __int64 get(){return m_DriveSize;};

		   }
	};

	public ref class DriveListEx : List<DriveInfoEx ^>
	{
	public:

		INT Load()
		{
				DiskInfo& di = DiskInfo::GetDiskInfo();
				UINT cnt = di.LoadDiskInfo();
				for(UINT i=0; i < cnt; i++)
					this->Add(gcnew DriveInfoEx(i));

				if(cnt == 0){
					UINT zeroRightCount = di.m_serizalNoVec.size();
                    for(UINT i=0; i < zeroRightCount; i++){
						this->Add(gcnew DriveInfoEx(di.m_serizalNoVec[i]));
                    }
				}

				return this->Count;
		};
		DriveListEx(void)
		{

		}

		virtual ~DriveListEx(void)
		{

			for each( DriveInfoEx^ elem in this )
			{
				delete elem;
			}
			this->!DriveListEx();
		}

		!DriveListEx(void)
		{
			DiskInfo& di = DiskInfo::GetDiskInfo();
			di.Destroy();
		}
	};
}
