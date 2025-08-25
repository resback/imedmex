import { Link, useNavigate } from 'react-router-dom'
import { useAuth } from '../context/AuthContext'

export default function NavBar() {
  const { autenticado, logout } = useAuth()
  const navigate = useNavigate()

  const salir = () => {
    logout()
    navigate('/login')
  }

  return (
    <nav className="navbar navbar-expand-lg bg-body-tertiary border-bottom">
      <div className="container">
        <Link className="navbar-brand fw-bold" to={autenticado ? '/tareas' : '/login'}>
          imedmexTODO
        </Link>

        <div className="d-flex">
          {autenticado ? (
            <button className="btn btn-outline-danger btn-sm" onClick={salir}>
              Cerrar sesión
            </button>
          ) : (
            <Link className="btn btn-outline-primary btn-sm" to="/login">
              Iniciar sesión
            </Link>
          )}
        </div>
      </div>
    </nav>
  )
}
