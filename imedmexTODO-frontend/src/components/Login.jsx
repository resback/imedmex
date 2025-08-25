import { useState } from 'react'
import api from '../services/api'
import { useAuth } from '../context/AuthContext'
import { useNavigate } from 'react-router-dom'

export default function Login() {
  const [usuario, setUsuario] = useState('prueba')
  const [contrasena, setContrasena] = useState('Prueba123*')
  const [cargando, setCargando] = useState(false)
  const [error, setError] = useState(null)

  const { login } = useAuth()
  const navigate = useNavigate()

  const enviar = async (e) => {
    e.preventDefault()
    setError(null)
    setCargando(true)
    try {
      const res = await api.post('/auth/login', { usuario, contrasena })
      // Respuesta estático-estandarizada: { exito, datos: { token, expira }, ... }
      const datos = res.data?.datos || res.data?.Datos // por si tu backend conserva PascalCase
      const token = datos?.token || datos?.Token
      if (!token) throw new Error('No se recibió token.')
      login(token)
      navigate('/tareas')
    } catch (err) {
      const msg = err?.response?.data?.mensaje || err.message || 'Error al iniciar sesión'
      setError(msg)
    } finally {
      setCargando(false)
    }
  }

  return (
    <div className="row justify-content-center">
      <div className="col-12 col-sm-8 col-md-6 col-lg-4">
        <div className="card shadow-sm">
          <div className="card-body">
            <h5 className="card-title mb-3">Iniciar sesión</h5>
            {error && <div className="alert alert-danger py-2">{error}</div>}
            <form onSubmit={enviar}>
              <div className="mb-3">
                <label className="form-label">Usuario</label>
                <input className="form-control" value={usuario} onChange={(e) => setUsuario(e.target.value)} />
              </div>
              <div className="mb-3">
                <label className="form-label">Contraseña</label>
                <input type="password" className="form-control" value={contrasena} onChange={(e) => setContrasena(e.target.value)} />
              </div>
              <button className="btn btn-primary w-100" disabled={cargando}>
                {cargando ? 'Entrando…' : 'Entrar'}
              </button>
            </form>
            <p className="text-muted small mt-3 mb-0">Usuario demo: <b>prueba</b> / <b>Prueba123*</b></p>
          </div>
        </div>
      </div>
    </div>
  )
}
