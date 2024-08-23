import React, { useEffect, useState } from 'react';
import { Button, Container, Grid, TextField, Typography } from '@material-ui/core';
import style from '../Tool/Style';
import { actualizarUsuario, obtenerUsuarioActual } from '../../actions/UsuarioAction';

const PerfilUsuario = () => {

    const [usuario, setUsuario] = useState({
        nombreCompleto: '',
        email: '',
        password: '',
        confirmarPassword: '',
        userName: ''
    });

    const ingresarValoresMemoria = e => {
        const { name, value } = e.target;
        setUsuario(anterior => ({
            ...anterior,
            [name]: value,
        }))
    }

    useEffect(() => {
        obtenerUsuarioActual().then(({data}) => {
            console.log('esta es la data del usuario', data); 
            setUsuario(anterior => ({
                ...anterior,
                nombreCompleto: data.nombreCompleto || '',
                email: data.email || '',
                password: data.password || '',
                confirmarPassword: data.confirmarPassword || '',
                userName: data.userName || ''
            }))
      })
    }, [])
    
    const guardarUsuario = e => {
        e.preventDefault();
        actualizarUsuario(usuario).then(({ data: { token } }) => {
            console.log('se actualizo el usuario: ', usuario)
            window.localStorage.setItem("token_seguridad", token)
        })
    }

    return (
        <Container component="main" maxWidth="md" justify="center">
            <div style={style.paper}>
                <Typography component="h1" variant="h5">
                    Perfil de Usuario
                </Typography>
            </div>
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
                            label="Nueva contraseña"
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
        </Container>
    );
}

export default PerfilUsuario