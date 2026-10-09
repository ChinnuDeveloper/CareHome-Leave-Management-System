 
import './App.css'
import { BrowserRouter, Routes, Route  } from 'react-router-dom'
import AppLayout from './components/layout/AppLayout' 
import StaffDashboardPage from './pages/StaffDashboardPage'
import LeaveRequestPage from './pages/LeaveRequestPage'
import EmployeesPage from './pages/EmployeesPage'
import EntitlementsPage from './pages/EntitlementsPage'
import LeaveApprovalPage from './pages/LeaveApprovalPage'

function App() { 

  return (
     <BrowserRouter>
     <Routes>
     <Route element={<AppLayout />}>
          <Route path="/" element={<StaffDashboardPage />} />
          <Route path="/staffdashboardpage" element={<StaffDashboardPage />} />
          <Route path="/leaverequestpage" element={<LeaveRequestPage />} />
          <Route path='/EmployeesPage' element={<EmployeesPage/>}/>
          <Route path='/EntitlementsPage' element={<EntitlementsPage/>}/>
          <Route path='/LeaveApprovalPage' element={<LeaveApprovalPage/>}/>
        </Route>
        </Routes>
     </BrowserRouter>
  )
}

export default App
