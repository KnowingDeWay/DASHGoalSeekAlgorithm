import { useState } from "react";
import styles from "../styles/gsaform.module.css";
import GSAFormInputField from "../components/GSAFormInputField";
import { GSAService } from "../services/gsa.service";
import { GoalSeekCalcRequest } from "../entities/GoalSeekCalcRequest";
import { GoalSeekCalcResponse } from "../entities/GoalSeekCalcResponse";

async function onButtonSubmit(
  e: any,
  setGsaResponse: any,
  reqModel: GoalSeekCalcRequest,
  setIsLoading: any
): Promise<void> {
  setIsLoading(true);
  e.preventDefault();
  const gsaService: GSAService = new GSAService();
  const response: GoalSeekCalcResponse = await gsaService.CalculateSolution(
    reqModel
  );
  setGsaResponse(response);
  setIsLoading(false);
}

interface GSAFormParams {
  setGsaResponse: any;
  setIsLoading: any;
}

export default function GSAForm({
  setGsaResponse,
  setIsLoading,
}: GSAFormParams) {
  const [formula, setFormula] = useState<string>("");
  const [initVal, setInitVal] = useState<string>("");
  const [targetResult, setTargetResult] = useState<string>("");
  const [maxIterations, setMaxIterations] = useState<string>("");

  return (
    <div className={styles.formContainer}>
      <form>
        <table>
          <tbody>
            <tr>
              <td>
                <GSAFormInputField
                  value={formula}
                  setValue={setFormula}
                  labelText="Enter Formula (use 'input' as variable)"
                  inputFieldId="formulaInput"
                  inputType="text"
                />
              </td>
              <td>
                <GSAFormInputField
                  value={initVal}
                  setValue={setInitVal}
                  labelText="Enter Initial Value (x0)"
                  inputFieldId="initValInput"
                  inputType="number"
                />
              </td>
            </tr>
            <tr>
              <td>
                <GSAFormInputField
                  value={targetResult}
                  setValue={setTargetResult}
                  labelText="Enter Target Result"
                  inputFieldId="targetResInput"
                  inputType="number"
                />
              </td>
              <td>
                <GSAFormInputField
                  value={maxIterations}
                  setValue={setMaxIterations}
                  labelText="Enter Max Number of Iterations"
                  inputFieldId="maxItrsInput"
                  inputType="number"
                />
              </td>
            </tr>
          </tbody>
        </table>
        <button
          className={styles.gsaFormSubmitBtn}
          onClick={(e) => {
            const reqModel: GoalSeekCalcRequest = {
              formula: formula,
              input: parseFloat(initVal),
              targetResult: parseFloat(targetResult),
              maximumIterations: parseInt(maxIterations),
            };
            onButtonSubmit(e, setGsaResponse, reqModel, setIsLoading);
          }}
        >
          Calculate Target Input
        </button>
      </form>
    </div>
  );
}
