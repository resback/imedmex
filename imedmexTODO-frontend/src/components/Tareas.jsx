import { useEffect, useMemo, useState } from 'react'
import api from '../services/api'

const prioridades = [
  { valor: 1, texto: 'Baja' },
  { valor: 2, texto: 'Media' },
  { valor: 3, texto: 'Alta' }
]

export default function Tareas() {
  const [tareas, setTareas] = useState([])
  const [cargando, setCargando] = useState(true)
  const [error, setError] = useState(null)

  // Form crear
  const [titulo, setTitulo] = useState('')
  const [descripcion, setDescripcion] = useState('')
  const [vencimiento, setVencimiento] = useState('')
  const [prioridad, setPrioridad] = useState(2)

  // edición simple en línea
  const [editId, setEditId] = useState(null)
  const [editData, setEditData] = useState({ titulo: '', descripcion: '', completada: false, fechaVencimiento: '', prioridad: 2 })

  const cargar = async () => {
    setCargando(true)
    setError(null)
    try {
      const res = await api.get('/tareas/listar')
      const datos = res.data?.datos || res.data?.Datos
      setTareas(Array.isArray(datos) ? datos : [])
    } catch (err) {
      const msg = err?.response?.data?.mensaje || 'Error al cargar tareas'
      setError(msg)
    } finally {
      setCargando(false)
    }
  }

  useEffect(() => {
    cargar()
  }, [])

  const crear = async (e) => {
    e.preventDefault()
    if (!titulo.trim()) return alert('El título es obligatorio.')
    try {
      const body = {
        titulo: titulo.trim(),
        descripcion: descripcion || null,
        fechaVencimiento: vencimiento ? `${vencimiento}T00:00:00` : null,
        prioridad: Number(prioridad)
      }
      await api.post('/tareas/crear', body)
      setTitulo(''); setDescripcion(''); setVencimiento(''); setPrioridad(2)
      await cargar()
    } catch (err) {
      alert(err?.response?.data?.mensaje || 'No se pudo crear la tarea')
    }
  }

  const completar = async (id) => {
    if (!confirm('¿Marcar como completada?')) return
    try {
      await api.patch(`/tareas/${id}/completar`)
      await cargar()
    } catch (err) {
      alert('Error al completar')
    }
  }

  const eliminar = async (id) => {
    if (!confirm('¿Eliminar tarea?')) return
    try {
      await api.delete(`/tareas/${id}`)
      await cargar()
    } catch (err) {
      alert('Error al eliminar')
    }
  }

  const editar = (t) => {
    setEditId(t.id)
    setEditData({
      titulo: t.titulo,
      descripcion: t.descripcion || '',
      completada: !!t.completada,
      fechaVencimiento: t.fechaVencimiento ? t.fechaVencimiento.substring(0, 10) : '',
      prioridad: t.prioridad || 2
    })
  }

  const guardarEdicion = async (id) => {
    try {
      const body = {
        titulo: editData.titulo.trim(),
        descripcion: editData.descripcion || null,
        completada: !!editData.completada,
        fechaVencimiento: editData.fechaVencimiento ? `${editData.fechaVencimiento}T00:00:00` : null,
        prioridad: Number(editData.prioridad)
      }
      await api.put(`/tareas/${id}/editar`, body)
      setEditId(null)
      await cargar()
    } catch (err) {
      alert(err?.response?.data?.mensaje || 'No se pudo editar')
    }
  }

  const cancelEdicion = () => {
    setEditId(null)
  }

  const totalPendientes = useMemo(() => tareas.filter(t => !t.completada).length, [tareas])

  return (
    <div className="row g-4">
      <div className="col-12">
        <div className="d-flex align-items-center justify-content-between">
          <h4 className="mb-0">Tareas</h4>
          <span className="badge text-bg-secondary">Pendientes: {totalPendientes}</span>
        </div>
      </div>

      <div className="col-12 col-lg-5">
        <div className="card shadow-sm">
          <div className="card-body">
            <h6 className="card-title mb-3">Crear tarea</h6>
            <form onSubmit={crear}>
              <div className="mb-2">
                <label className="form-label">Título *</label>
                <input className="form-control" value={titulo} onChange={e => setTitulo(e.target.value)} />
              </div>
              <div className="mb-2">
                <label className="form-label">Descripción</label>
                <textarea className="form-control" value={descripcion} onChange={e => setDescripcion(e.target.value)} rows={2}></textarea>
              </div>
              <div className="row">
                <div className="col-6 mb-2">
                  <label className="form-label">Vence</label>
                  <input type="date" className="form-control" value={vencimiento} onChange={e => setVencimiento(e.target.value)} />
                </div>
                <div className="col-6 mb-2">
                  <label className="form-label">Prioridad</label>
                  <select className="form-select" value={prioridad} onChange={e => setPrioridad(e.target.value)}>
                    {prioridades.map(p => <option key={p.valor} value={p.valor}>{p.texto}</option>)}
                  </select>
                </div>
              </div>
              <button className="btn btn-primary">Guardar</button>
            </form>
          </div>
        </div>
      </div>

      <div className="col-12 col-lg-7">
        <div className="card shadow-sm">
          <div className="card-body">
            <h6 className="card-title mb-3">Listado</h6>
            {error && <div className="alert alert-danger">{error}</div>}
            {cargando ? (
              <div>Cargando…</div>
            ) : (
              <div className="table-responsive">
                <table className="table table-sm align-middle">
                  <thead>
                    <tr>
                      <th style={{width: '38%'}}>Título</th>
                      <th>Prioridad</th>
                      <th>Vence</th>
                      <th>Estado</th>
                      <th className="text-end">Acciones</th>
                    </tr>
                  </thead>
                  <tbody>
                    {tareas.map(t => (
                      <tr key={t.id}>
                        <td>
                          {editId === t.id ? (
                            <input className="form-control form-control-sm" value={editData.titulo} onChange={e => setEditData(d => ({...d, titulo: e.target.value}))} />
                          ) : (
                            <>
                              <div className={t.completada ? 'text-decoration-line-through' : ''}>{t.titulo}</div>
                              {t.descripcion && <div className="small text-muted">{t.descripcion}</div>}
                            </>
                          )}
                        </td>
                        <td>
                          {editId === t.id ? (
                            <select className="form-select form-select-sm" value={editData.prioridad} onChange={e => setEditData(d => ({...d, prioridad: e.target.value}))}>
                              {prioridades.map(p => <option key={p.valor} value={p.valor}>{p.texto}</option>)}
                            </select>
                          ) : (
                            ['','Baja','Media','Alta'][t.prioridad]
                          )}
                        </td>
                        <td>
                          {editId === t.id ? (
                            <input type="date" className="form-control form-control-sm" value={editData.fechaVencimiento} onChange={e => setEditData(d => ({...d, fechaVencimiento: e.target.value}))} />
                          ) : (
                            t.fechaVencimiento ? t.fechaVencimiento.substring(0,10) : '-'
                          )}
                        </td>
                        <td>
                          {editId === t.id ? (
                            <div className="form-check">
                              <input id={`chk-${t.id}`} className="form-check-input" type="checkbox" checked={editData.completada} onChange={e => setEditData(d => ({...d, completada: e.target.checked}))} />
                              <label className="form-check-label" htmlFor={`chk-${t.id}`}>Completada</label>
                            </div>
                          ) : (
                            t.completada ? <span className="badge text-bg-success">Hecha</span> : <span className="badge text-bg-warning">Pendiente</span>
                          )}
                        </td>
                        <td className="text-end">
                          {editId === t.id ? (
                            <>
                              <button className="btn btn-sm btn-success me-2" onClick={() => guardarEdicion(t.id)}>Guardar</button>
                              <button className="btn btn-sm btn-secondary" onClick={cancelEdicion}>Cancelar</button>
                            </>
                          ) : (
                            <>
                              <button className="btn btn-sm btn-outline-primary me-2" onClick={() => editar(t)}>  <i class="bi bi-pencil-square"></i> </button>
                              {!t.completada && <button className="btn btn-sm btn-outline-success me-2" onClick={() => completar(t.id)}>
                                <i class="bi bi-check-circle"></i></button>}
                              <button className="btn btn-sm btn-outline-danger" onClick={() => eliminar(t.id)}>
                                  <i class="fa-solid fa-trash"></i>

                              </button>
                            </>
                          )}
                        </td>
                      </tr>
                    ))}
                    {tareas.length === 0 && (
                      <tr>
                        <td colSpan={5} className="text-center text-muted">Sin tareas aún</td>
                      </tr>
                    )}
                  </tbody>
                </table>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  )
}
