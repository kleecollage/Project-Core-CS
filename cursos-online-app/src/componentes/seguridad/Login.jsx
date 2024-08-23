import React, { useState } from 'react';
import style from '../Tool/Style';
import { Avatar, Button, Container, TextField, Typography } from '@material-ui/core';
import LockTwoToneIcon from '@material-ui/icons/LockTwoTone';
import { loginUsuario } from '../../actions/UsuarioAction';

const Login = () => {

    const [usuario, setUsuario] = useState({
        Email: '',
        Password: ''
    })

    const ingresarValoresMemoria = e => {
        const { name, value } = e.target;
        setUsuario(anterior => ({
            ...anterior,
            [name]: value
        }))
    }
    

    /* test user
    * moni.ramirez@gmail.com
    * Jwtpass123$
    */ 
    const loginUsuarioBoton = e => {
        e.preventDefault();
        loginUsuario(usuario).then( ({data}) => {
            console.log('login exitoso:', data);
            window.localStorage.setItem("token_seguridad", data.token)
        });
    }


    return (
        <Container maxWidth="xs">
            <div style={style.paper}>
                <Avatar style={style.avatar}>
                    <LockTwoToneIcon style={style.icon} />
                </Avatar>
                <Typography component="h1" variant="h5">
                    Login Usuario
                </Typography>
                <form style={style.form}>
                    <TextField
                        variant='outlined'
                        name='Email'
                        value={usuario.Email}
                        onChange={ingresarValoresMemoria}
                        label="Ingrese su email"
                        fullWidth
                        margin='normal'
                    />

                    <TextField
                        variant='outlined'
                        name='Password'
                        value={usuario.Password}
                        onChange={ingresarValoresMemoria}
                        type='password'
                        label="Ingrese su contraseña"
                        fullWidth
                        margin='normal'
                    />

                    <Button
                        type='submit'
                        onClick={loginUsuarioBoton}
                        fullWidth
                        variant='contained'
                        color='primary'
                        style={style.submit}
                    >
                        Ingresar
                    </Button>

                </form>
            </div>
        </Container>
    );
};

export default Login;