import HttpCliente from '../servicios/HttpCliente'

export const registrarUsuario = usuario => {
    return new Promise((resolve, reject) => {
        HttpCliente.post('/Usuario/registrar', usuario).then(response => {
            resolve(response)
        })
    })
}

export const obtenerUsuarioActual = () => {
    return new Promise((resolve, reject) => {
        HttpCliente.get('/Usuario').then(response => {
            resolve(response)
        })
    })
}

export const actualizarUsuario = (usuario) => {
    return new Promise((resolve, reject) => {
    HttpCliente.put('/Usuario', usuario).then(response => {
            resolve(response)
        })
    })
}

export const loginUsuario = usuario => {
    return new Promise((resolve, reject) => {
        HttpCliente.post('/Usuario/login', usuario).then(response => {
            resolve(response)
        })
    })
}