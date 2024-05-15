import { GoalSeekCalcResponse } from "./../entities/GoalSeekCalcResponse";
import axios from "axios";
import { GoalSeekCalcRequest } from "../entities/GoalSeekCalcRequest";

export class GSAService {
  private apiBaseAddress: string = `${import.meta.env.VITE_API_BASE_ADDRESS}${
    import.meta.env.VITE_API_PORT
  }`;
  private calculateGsaUrl: string = `${this.apiBaseAddress}/api/goalseek`;

  public async CalculateSolution(
    requestModel: GoalSeekCalcRequest
  ): Promise<GoalSeekCalcResponse> {
    let response: GoalSeekCalcResponse = {
      targetInput: null,
      iterationsRequired: null,
    };
    await axios
      .post(this.calculateGsaUrl, requestModel, {})
      .then(function (res) {
        response = res.data;
      })
      .catch(function (err) {
        if (err.response.status === 400) {
          alert(`Error: Ensure that formula uses 'input' phrase as variable and that 
            this formula is a mathematical function that uses strictly +, -, *, / operators. 
            Other fields should be strictly numeric`);
        } else {
          alert(
            `Error: An internal server error has occured. Please try again`
          );
        }
      });
    return response;
  }
}
