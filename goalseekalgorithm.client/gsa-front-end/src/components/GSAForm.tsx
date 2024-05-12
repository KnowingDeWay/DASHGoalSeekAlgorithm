import { useState } from "react";
import styles from "../styles/gsaform.module.css";
import GSAFormInputField from "../components/GSAFormInputField";

export default function GSAForm() {
  const [formula, setFormula] = useState<string>("");
  const [initVal, setInitVal] = useState<string>("");
  const [targetResult, setTargetResult] = useState<string>("");
  const [maxIterations, setMaxIterations] = useState<string>("");

  return (
    <div className={styles.formContainer}>
      <form>
        <table>
          <tr>
            <td>
              <GSAFormInputField
                value={formula}
                setValue={setFormula}
                labelText="Enter Formula (use 'input' as variable)"
                inputFieldId="formulaInput"
              />
            </td>
            <td>
              <GSAFormInputField
                value={initVal}
                setValue={setInitVal}
                labelText="Enter Initial Value (x0)"
                inputFieldId="initValInput"
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
              />
            </td>
            <td>
              <GSAFormInputField
                value={maxIterations}
                setValue={setMaxIterations}
                labelText="Enter Max Number of Iterations"
                inputFieldId="maxItrsInput"
              />
            </td>
          </tr>
        </table>
        <button className={styles.gsaFormSubmitBtn}>
          Calculate Target Input
        </button>
      </form>
    </div>
  );
}
