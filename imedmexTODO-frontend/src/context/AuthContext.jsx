import React, { createContext, useContext, useEffect, useState } from 'react'
import { setAuthToken } from '../services/api'

const AuthContext = createContext(null)

export function AuthProvider({ children }) {
  const [token, setToken] = useState(() => localStorage.getItem('token'))

  useEffect(() => {
    setAuthToken(token)
  }, [token])

  const login = (nuevoToken) => {
    setToken(nuevoToken)
    localStorage.setItem('token', nuevoToken)
    setAuthToken(nuevoToken)
  }

  const logout = () => {
    setToken(null)
    localStorage.removeItem('token')
    setAuthToken(null)
  }

  return (
    <AuthContext.Provider value={{ token, autenticado: !!token, login, logout }}>
      {children}
    </AuthContext.Provider>
  )
}

export const useAuth = () => useContext(AuthContext)
