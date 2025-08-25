import { Routes, Route, Navigate } from 'react-router-dom'
import NavBar from './components/NavBar'
import Login from './components/Login'
import Tareas from './components/Tareas'
import ProtectedRoute from './components/ProtectedRoute'
import { useAuth } from './context/AuthContext'

export default function App() {
  const { autenticado } = useAuth()

  return (
    <>
      <NavBar />
      <div className="container py-4">
        <Routes>
          <Route path="/login" element={autenticado ? <Navigate to="/tareas" /> : <Login />} />
          <Route
            path="/tareas"
            element={
              <ProtectedRoute>
                <Tareas />
              </ProtectedRoute>
            }
          />
          <Route path="*" element={<Navigate to={autenticado ? '/tareas' : '/login'} />} />
        </Routes>
      </div>
    </>
  )
}
