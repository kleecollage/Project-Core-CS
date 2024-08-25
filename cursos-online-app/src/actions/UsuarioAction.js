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
            .then(({data}) => {
                // console.log('response: ', response)
                if (data && data.imagenPerfil) {
                    let fotoPerfil = data.imagenPerfil;
                    const nuevoFile = 'data:image/' + fotoPerfil.extension + ';base64,' + fotoPerfil.data;
                    data.imagenPerfil = nuevoFile
                }
                if (typeof (dispatch) == 'function') {
                    dispatch({
                        type: "INICIAR_SESION",
                        sesion: data,
                        autenticado: true
                    });
                }
                resolve(data)
            })
            .catch(error => {
                console.log("Error al obtener usuario", error)
                reject(error)
            });
    })
}

export const actualizarUsuario = (usuario, dispatch) => {
    return new Promise((resolve, reject) => {
        HttpCliente.put('/Usuario', usuario)
            .then((response) => {
                 if (response.data && response.data.imagenPerfil) {
                    let fotoPerfil = response.data.imagenPerfil;
                    const nuevoFile = 'data:image/' + fotoPerfil.extension + ';base64,' + fotoPerfil.data;
                    response.data.imagenPerfil = nuevoFile
                }
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
                reject (error)
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