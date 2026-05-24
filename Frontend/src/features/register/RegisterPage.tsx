import { useState } from "react";
import PublicLayout from "../../Components/PublicLayout";
import UserDataRegister from "./UserDataRegister";
import UserConfigurationRegister from "./UserConfigurationRegister";
import { useForm, type SubmitHandler } from "react-hook-form";
import { navigate } from "wouter/use-hash-location";

export interface UserRegisterForm {
  email: string;
  password: string;
  repeatedPassword: string;
  username: string;
  timeZone: string;
  minGoal: number;
  cutOffTime: number;
}

export interface UserRegisterRequest {
  email: string;
  password: string;
  username: string;
  timeZone: string;
  minGoal: number;
  cutOffTime: number;
}

export default function RegisterPage() {
  const { register, handleSubmit, formState, trigger, control } =
    useForm<UserRegisterForm>({
      defaultValues: {
        timeZone: Intl.DateTimeFormat().resolvedOptions().timeZone,
      },
      reValidateMode: "onSubmit",
    });

  const [steps, setSteps] = useState(2);
  const [fetchError, setFetchError] = useState("");

  const onContinueForm = () => {
    setSteps((s) => s + 1);
  };

  const onPreviousForm = () => {
    setSteps((s) => s - 1);
  };

  //Moverlo a un hook.
  const onSubmit: SubmitHandler<UserRegisterForm> = async (data) => {
    const requestData: UserRegisterRequest = {
      email: data.email,
      username: data.username,
      password: data.password,
      minGoal: data.minGoal,
      cutOffTime: data.cutOffTime,
      timeZone: data.timeZone,
    };

    try {
      const request = await fetch("https://localhost:7074/api/users/register", {
        method: "post",
        body: JSON.stringify(requestData),
      });

      const response = await request.json();

      if (!request.ok) 
    } catch (error) {
      console.log(error);
    }

    navigate("/dashboard");
  };

  return (
    <PublicLayout header="Creación de cuenta">
      <form onSubmit={handleSubmit(onSubmit)}>
        {steps == 1 ? (
          <UserDataRegister
            trigger={trigger}
            register={register}
            formState={formState}
            onContinueForm={onContinueForm}
          ></UserDataRegister>
        ) : (
          <UserConfigurationRegister
            register={register}
            onPreviousForm={onPreviousForm}
            formState={formState}
          ></UserConfigurationRegister>
        )}
      </form>
    </PublicLayout>
  );
}
