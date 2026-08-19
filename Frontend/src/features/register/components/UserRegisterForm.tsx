import { useForm, type SubmitHandler } from "react-hook-form";
import { useLocation } from "wouter";
import type { UserRegisterForm } from "../types/RegisterTypes";
import PublicLayout from "../../../components/PublicLayout";
import useUser from "../../../hooks/useUser";
import styles from "./UserRegisterForm.module.css";
import FormInput from "../../../components/FormInput";

export default function UserRegisterForm() {
  const {
    register,
    handleSubmit,
    formState: { errors },
    setError,
  } = useForm<UserRegisterForm>();

  const [, setLocation] = useLocation();
  const { registerUser, loginUser } = useUser();
  //Moverlo a un hook.
  const submitRegister: SubmitHandler<UserRegisterForm> = async (data) => {
    try {
      const response = await registerUser(data);

      if (response.error) {
        setError("root.serverError", {
          type: "manual",
          message: response.error.detail,
        });

        return;
      }

      await loginUser({ username: data.username, password: data.password });
      setLocation("/account-configuration");
    } catch (error: unknown) {
      setError("root.serverError", {
        type: "manual",
        message: "Hubo un error con el servidor. Vuelva pronto",
      });
    }
  };

  return (
    <PublicLayout>
      <div className={styles.containerForm}>
        <h2 className={styles.titleForm}>Crea tu cuenta</h2>
        <form onSubmit={handleSubmit(submitRegister)} className={styles.form}>
          <div className={styles.inputsContainerForm}>
            <FormInput
              type="email"
              label="Correo"
              error={errors.email?.message}
              {...register("email", {
                required: {
                  value: true,
                  message: "Ingresa un correo",
                },
                pattern: {
                  value: /.@./g,
                  message: "Ingresa un correo valido",
                },
              })}
            ></FormInput>
            <FormInput
              label="Nombre de usuario"
              error={errors.username?.message}
              {...register("username", {
                required: {
                  value: true,
                  message: "Ingresa un nombre de usuario",
                },
                minLength: {
                  value: 4,
                  message: "Tu nombre debe tener al menos 4 caracteres",
                },
                maxLength: {
                  value: 32,
                  message: "Tu nombre no puede tener mas de 32 caracteres",
                },
              })}
            ></FormInput>
            <FormInput
              label="Contraseña"
              type="password"
              error={errors.password?.message}
              {...register("password", {
                required: {
                  value: true,
                  message: "Ingresa una contraseña",
                },
                minLength: {
                  value: 8,
                  message: "Tu contraseña debe tener minimo 8 caracteres",
                },
              })}
            ></FormInput>
            <FormInput
              label="Repite tu contraseña"
              type="password"
              error={errors.repeatedPassword?.message}
              {...register("repeatedPassword", {
                required: {
                  value: true,
                  message: "Debes ingresar nuevamente tu contraseña",
                },
                validate: {
                  checkPasswords: (repeatedPassword, { password }) => {
                    if (repeatedPassword !== password)
                      return "Las dos contraseñas no son iguales";
                  },
                },
              })}
            ></FormInput>
          </div>
          <input
            type="submit"
            value="Registrarse"
            className={styles.btnSubmit}
          />
        </form>
      </div>
    </PublicLayout>
  );
}
