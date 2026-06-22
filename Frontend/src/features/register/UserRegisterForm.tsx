import PublicLayout from "../../components/PublicLayout";
import { useForm, type SubmitHandler } from "react-hook-form";
import type { UserRegisterForm } from "../../types/RegisterTypes";
import { useLocation } from "wouter";
import useUser from "../../hooks/useUser";

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
    const response = await registerUser(data);

    if (response.status >= 400) {
      setError("root.serverError", {
        type: "manual",
        message: response.detail,
      });

      return;
    }

    await loginUser({ username: data.username, password: data.password });
    //TODO: check if there's a mistake.

    //TODO: ey, wait so i can login user.
    setLocation("/account-configuration");
  };

  return (
    <PublicLayout header="Creación de cuenta">
      <form onSubmit={handleSubmit(submitRegister)}>
        <div>
          <h2>Crea tu cuenta</h2>
        </div>
        <div>
          <div>
            <label htmlFor="">Correo</label>
            <input
              {...register("email", {
                required: {
                  value: true,
                  message: "El campo correo esta vacio",
                },
                pattern: { value: /.@./g, message: "Ingresa un correo valido" },
              })}
              type="email"
            />
          </div>
          <div>
            <label htmlFor="">Nombre de usuario</label>
            <input
              type="text"
              {...register("username", {
                required: {
                  value: true,
                  message: "El campo nombre de usuario esta vacio",
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
            />
          </div>
          <div>
            <label htmlFor="">Contraseña</label>
            <input
              type="password"
              {...register("password", {
                required: {
                  value: true,
                  message: "Debes ingresar una contraseña",
                },
                minLength: {
                  value: 8,
                  message: "Tu contraseña debe tener minimo 8 caracteres",
                },
              })}
            />
          </div>
          <div>
            <label htmlFor="">Repite tu contraseña</label>
            <input
              type="password"
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
            />
          </div>
        </div>
        <ul>
          {errors?.root?.serverError && (
            <li>{errors.root.serverError.message}</li>
          )}
          {errors.email && <li>{errors.email?.message}</li>}
          {errors.username && <li>{errors.username?.message}</li>}
          {errors.password && <li>{errors.password?.message}</li>}
          {errors.repeatedPassword && (
            <li>{errors.repeatedPassword?.message}</li>
          )}
        </ul>
        <input type="submit" value="Registrarse" />
      </form>
    </PublicLayout>
  );
}
