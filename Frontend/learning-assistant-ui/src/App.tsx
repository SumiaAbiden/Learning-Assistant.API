import './App.css'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Dashboard from './pages/Dashboard'
import Topics from './pages/Topics'
import Journal from './pages/Journal'
import MainLayout from './layouts/MainLayout'

function App() {
  return (
    <BrowserRouter>
      <MainLayout>
        <Routes>
          <Route path="/" element={<Dashboard />} />
          <Route path="/topics" element={<Topics />} />
          <Route path="/journal" element={<Journal />} />
        </Routes>
      </MainLayout>
    </BrowserRouter>
  )
}

export default App