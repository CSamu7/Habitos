import {
  useFormState,
  type FormState,
  type UseFormRegister,
  type UseFormTrigger,
} from "react-hook-form";
import type { UserRegisterForm } from "./RegisterPage";

export default function UserDataRegister({
  register,
  trigger,
  formState,
  onContinueForm,
}: {
  register: UseFormRegister<UserRegisterForm>;
  trigger: UseFormTrigger<UserRegisterForm>;
  formState: FormState<UserRegisterForm>;
  onContinueForm: () => void;
}) {
  const errors = formState.errors;

  return (
    <div>
      <div>
        <h2>Crea tu cuenta</h2>
      </div>
      <div>
        <div>
          <label htmlFor="">Correo</label>
          <input
            {...register("email", {
              required: { value: true, message: "El campo correo esta vacio" },
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
        {errors.email && <li>{errors.email?.message}</li>}
        {errors.username && <li>{errors.username?.message}</li>}
        {errors.password && <li>{errors.password?.message}</li>}
        {errors.repeatedPassword && <li>{errors.repeatedPassword?.message}</li>}
      </ul>
      <button
        type="button"
        onClick={async (e) => {
          const isValid = await trigger([
            "username",
            "email",
            "password",
            "repeatedPassword",
          ]);

          if (isValid) onContinueForm();
        }}
      >
        Continuar
      </button>
    </div>
  );
}
