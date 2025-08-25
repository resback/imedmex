import axios from 'axios'

const api = axios.create({
  baseURL: '/api', // se proxea a https://localhost:7298 por vite.config.js
  headers: { 'Content-Type': 'application/json' }
})

export function setAuthToken(token) {
  if (token) {
    api.defaults.headers.common['Authorization'] = `Bearer ${token}`
  } else {
    delete api.defaults.headers.common['Authorization']
  }
}

// Manejo simple de 401 -> limpiar y mandar a /login
api.interceptors.response.use(
  (r) => r,
  (err) => {
    if (err?.response?.status === 401) {
      localStorage.removeItem('token')
      window.location = '/login'
    }
    return Promise.reject(err)
  }
)

export default api
