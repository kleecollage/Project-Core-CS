import Axios from 'axios'
import HttpCliente from '../servicios/HttpCliente'

const instancia = Axios.create();
instancia.CancelToken = Axios.CancelToken;
instancia.isCancel = Axios.isCancel;


export const registrarUsuario = usuario => {
    return new Promise((resolve, reject) => {
        instancia.post('/Usuario/registrar', usuario).then(response => {
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

export const loginUsuario = (usuario, dispatch) => {
    return new Promise((resolve, reject) => {
        instancia.post('/Usuario/login', usuario).then(response => {

            if (response.data && response.data.imagenPerfil) {
                let fotoPerfil = response.data.imagenPerfil;
                const nuevoFile = "data:image/" + fotoPerfil.extension + ";base64," + fotoPerfil.data;
                response.data.imagenPerfil = nuevoFile
            }

            dispatch({
                type: "INICIAR_SESION",
                sesion: response.data,
                autenticado: true
            })
            resolve(response)
        }).catch(error => {
            resolve(error.response)
        })
    })
}