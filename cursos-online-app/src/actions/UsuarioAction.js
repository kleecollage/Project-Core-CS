import HttpCliente from '../servicios/HttpCliente'

export const registrarUsuario = usuario => {
    return new Promise((resolve, reject) => {
        HttpCliente.post('/Usuario/registrar', usuario).then(response => {
            resolve(response)
        })
    })
}

export const obtenerUsuarioActual = (dispatch) => {
    return new Promise((resolve, reject) => {
        HttpCliente.get('/Usuario')
            .then(response => {
                if (typeof (dispatch) == 'function') {
                    dispatch({
                        type: "INICIAR_SESION",
                        sesion: response.data,
                        autenticado: true
                    });
                }
                resolve(response)
            })
            .catch(error => {
                reject(error.response)
            });
    })
}

export const actualizarUsuario = (usuario) => {
    return new Promise((resolve, reject) => {
        HttpCliente.put('/Usuario', usuario)
            .then(response => {
                resolve(response)
            })
            .catch(error => {
                reject (error.response)
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