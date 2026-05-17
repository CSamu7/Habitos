import {
  type FieldErrors,
  type SubmitHandler,
  type UseFormHandleSubmit,
  type UseFormRegister,
} from "react-hook-form";
import type { UserRegisterForm } from "./RegisterPage";

export default function RegisterStepOne({
  register,
  errors,
  onContinueForm,
  onSubmit,
}: {
  register: UseFormRegister<UserRegisterForm>;
  errors: FieldErrors<UserRegisterForm>;
  onContinueForm: () => void;
  onSubmit: UseFormHandleSubmit<UserRegisterForm, UserRegisterForm>;
}) {
  const onSumbitfd: SubmitHandler<UserRegisterForm> = (data) => {
    onContinueForm();
  };

  return (
    <form onSubmit={() => onSubmit(onSumbitfd)}>
      <div>
        <h2>Crea tu cuenta</h2>
      </div>
      <div>
        <div>
          <label htmlFor="">Correo</label>
          <input type="email" {...(register("email"), { required: true })} />
        </div>
        <div>
          <label htmlFor="">Nombre de usuario</label>
          <input
            type="text"
            {...(register("username"), { required: true, minLength: 4 })}
          />
          {errors.username && <p>{errors.username.message}</p>}
        </div>
        <div>
          <label htmlFor="">Contraseña</label>
          <input
            type="password"
            {...(register("password"), { required: true })}
          />
        </div>
        <div>
          <label htmlFor="">Repite tu contraseña</label>
          <input
            type="password"
            {...(register("passwordAgain"), { required: true })}
          />
        </div>
      </div>
      <div>
        <p>
          {errors.email?.message}
          {errors.username?.message}
          {errors.password?.message}
          {errors.passwordAgain?.message}
        </p>
      </div>
      <input type="submit" />
    </form>
  );
}
