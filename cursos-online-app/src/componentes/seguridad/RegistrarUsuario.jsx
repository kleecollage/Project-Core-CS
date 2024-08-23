import { Button, Container, Grid, TextField, Typography } from "@material-ui/core";
import React, { useState } from "react";
import style from "../Tool/Style";
import { registrarUsuario } from "../../actions/UsuarioAction";

const RegitrarUsuario = () => {

    const [usuario, setUsuario] = useState({
        NombreCompleto: '',
        Email: '',
        Password: '',
        ConfirmarPassword: '',
        Username: ''
    })

    const ingresarValoresMemoria = e => {
        const { name, value } = e.target;
        setUsuario(anterior => ({
            ...anterior,
            [name]: value
        }))
    }

    const registrarUsuarioBoton = e => {
        e.preventDefault();
        registrarUsuario(usuario).then(({data}) => {
            console.log('se registro exitosamente el usuario', data)
            window.localStorage.setItem("token_seguridad", data.token)
        })
    }

    return (
        <Container component="main" maxWidth="md" justify="center">
            <div style={style.paper}>
                <Typography component="h1" variant="h5">
                    Registro de Usuario
                </Typography>
                <form style={style.form}>
                    <Grid container spacing={2}>
                        <Grid item xs={12} md={12}>
                            <TextField
                                name="NombreCompleto"
                                value={usuario.NombreCompleto}
                                onChange={ingresarValoresMemoria}
                                variant="outlined"
                                fullWidth
                                label="Ingrese su nombre y apellidos"
                            />
                        </Grid>
                        
                        <Grid item xs={12} md={6}>
                            <TextField
                                name="Email"
                                value={usuario.Email}
                                onChange={ingresarValoresMemoria}
                                variant="outlined"
                                type="email"
                                fullWidth
                                label="Ingrese su email"
                            />
                        </Grid>
                        
                        <Grid item xs={12} md={6}>
                            <TextField
                                name="Username"
                                value={usuario.Username}
                                onChange={ingresarValoresMemoria}
                                variant="outlined"
                                fullWidth
                                label="Ingrese su username"
                            />
                        </Grid>
                        
                        <Grid item xs={12} md={6}>
                            <TextField
                                name="Password"
                                value={usuario.Password}
                                onChange={ingresarValoresMemoria}
                                type="password"
                                variant="outlined"
                                fullWidth
                                label="Ingrese su constraseña"
                            />
                        </Grid>
                        
                        <Grid item xs={12} md={6}>
                            <TextField
                                name="ConfirmarPassword"
                                value={usuario.ConfirmarPassword}
                                onChange={ingresarValoresMemoria}
                                type="password"
                                variant="outlined"
                                fullWidth
                                label="Confirme su constraseña"
                            />
                        </Grid>

                        <Grid container justify="center">
                            <Grid item xs={12} md={6}>
                                <Button
                                    type="submit"
                                    onClick={registrarUsuarioBoton}
                                    fullWidth
                                    variant="contained"
                                    color="primary"
                                    size="large"
                                    style={style.submit}
                                >
                                    Enviar
                                </Button>
                            </Grid>
                        </Grid>

                    </Grid>
                </form>
            </div>
        </Container>
    );
}

export default RegitrarUsuario;