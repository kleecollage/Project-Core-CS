import React, { useEffect, useState } from 'react';
import ImageUploader from 'react-images-upload';
import { v4 as uuidv4 } from 'uuid';
import { Avatar, Button, Container, Grid, TextField, Typography } from '@material-ui/core';
import { actualizarUsuario } from '../../actions/UsuarioAction';
import { useStateValue } from "../../contexto/store";
import style from '../Tool/Style';
import defaultFoto from "../../logo.jpg";
import { obtenerDataImagen } from '../../actions/ImagenAction';

const PerfilUsuario = () => {
    const [{ sesionUsuario }, dispatch] = useStateValue();
    const [usuario, setUsuario] = useState({
        nombreCompleto: '',
        email: '',
        password: '',
        confirmarPassword: '',
        userName: '',
        imagenPerfil: null,
        // foto: { data: '', nombre: '', extension: '' },
        fotoUrl: '',
    });

    const ingresarValoresMemoria = e => {
        const { name, value } = e.target;
        setUsuario(anterior => ({
            ...anterior,
            [name]: value,
        }))
    }

    useEffect(() => {
        setUsuario(sesionUsuario.usuario);
        setUsuario(anterior => ({
            ...anterior,
            password: '',
            confirmarPassword: '',
            fotoUrl: sesionUsuario.usuario.imagenPerfil
        }))
    }, [])
    
    const guardarUsuario = e => {
        e.preventDefault();
        actualizarUsuario(usuario, dispatch).then((response) => {
            console.log("response", response)
            if (response.status === 200) {
                dispatch({
                    type: "OPEN_SNACKBAR",
                    openMensaje: {
                        open: true,
                        mensaje: "¡Se guardaron los cambios existosamente!"
                    }
                });
                window.localStorage.setItem("token_seguridad", response.data.token)
            } else {
                dispatch({
                    type: "OPEN_SNACKBAR",
                    openMensaje: {
                        open: true,
                        mensaje: "Errores al guardar usuario en: " + Object.keys(response)
                    }
                })
            }
            console.log('se actualizo el usuario: ', response)
        })
    }

    const subirFoto = imagenes => {
        const foto = imagenes[0]; // typeOf: File
        const fotoUrl = URL.createObjectURL(foto); // typeOf: URL

        obtenerDataImagen(foto).then(respuesta => {
            console.log(respuesta);
            setUsuario(anterior => ({
                ...anterior,
                imagenPerfil: respuesta, // JSON {data: ..., nombre: ..., extension: ....}
                fotoUrl: fotoUrl
           })) 
        });
    }

    const fotoKey = uuidv4();

    return (
        <Container component="main" maxWidth="md" justify="center">
            <div style={style.paper}>
                <Avatar style={style.avatar} src={ usuario.fotoUrl || defaultFoto} />
                <Typography component="h1" variant="h5">
                    Perfil de Usuario
                </Typography>
            
                <form style={style.form}>
                    <Grid container spacing={2}>
                        <Grid item xs={12} md={6}>
                            <TextField
                                name='nombreCompleto'
                                value={usuario.nombreCompleto}
                                onChange={ingresarValoresMemoria}
                                variant='outlined'
                                fullWidth
                                label="Ingrese su nombre y apellidos"
                            />
                        </Grid>
                        
                        <Grid item xs={12} md={6}>
                            <TextField
                                name='userName'
                                value={usuario.userName}
                                onChange={ingresarValoresMemoria}
                                variant='outlined'
                                fullWidth
                                label="Ingrese su username"
                            />
                        </Grid>
                        
                        <Grid item xs={12} md={6}>
                            <TextField
                                name='email'
                                value={usuario.email}
                                onChange={ingresarValoresMemoria}
                                variant='outlined'
                                type='email'
                                fullWidth
                                label="Email"
                                disabled
                            />
                        </Grid>

                        <Grid item xs={12} md={6}>
                            <TextField
                                name='password'
                                value={usuario.password}
                                onChange={ingresarValoresMemoria}
                                variant='outlined'
                                type='password'
                                fullWidth
                                label="Ingrese contraseña"
                            />
                        </Grid>

                        <Grid item xs={12} md={6}>
                            <TextField
                                name='confirmarPassword'
                                value={usuario.confirmarPassword}
                                onChange={ingresarValoresMemoria}
                                variant='outlined'
                                type='password'
                                fullWidth
                                label="Confirme su contraseña"
                            />
                        </Grid>

                        <Grid item xs={12} md={6}>
                            <ImageUploader
                                withIcon={false}
                                key={fotoKey}
                                singleImage={true}
                                buttonText='Seleccione una imagen de perfil'
                                onChange={subirFoto}
                                imgExtension={[".jpg", ".png", ".gif", ".jpeg"]}
                                maxFileSize={5242880}
                            />
                        </Grid>

                        <Grid container justify='center'>
                            <Grid item xs={12} md={6}>
                                <Button
                                    type='submit'
                                    onClick={guardarUsuario}
                                    fullWidth
                                    variant='contained'
                                    size='large'
                                    color='primary'
                                    style={style.submit}
                                >
                                    Guardar Datos
                                </Button>
                            </Grid>
                        </Grid>
                    </Grid>
                </form>
            </div>
        </Container>
    );
}

export default PerfilUsuario