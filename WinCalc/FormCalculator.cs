using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinCalc {
    
    /// <summary>
    /// program: windows calculator
    /// Auteur: Yannick Gervais
    /// Note: il est possible d'utiliser le clavier pour entrer les chiffres. Voir la function 
    ///       FormCalculator_KeyPress(object sender, KeyPressEventArgs e) pour savoir quelle touchent sont actives
    ///
    /// </summary>
    public partial class FormCalculator : Form {
        double value = 0d;
        string operation = "";
        bool operationPress = false;
        bool equalPressed = false;
        
        public FormCalculator() {
            InitializeComponent();
        }

        /// <summary>
        /// click event for numbers and ","
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNumber_Click(object sender, EventArgs e) {      
            if (result.Text == "0" || operationPress || equalPressed) {
                result.Clear();
                equalPressed = false;
            }            
            operationPress = false;
            Button buttonPressed = (Button)sender;
            
            if(buttonPressed.Text == ","){
                if (!result.Text.Contains(",")) {
                    result.Text = result.Text + buttonPressed.Text;
                }
            }
            else{                
                result.Text = result.Text + buttonPressed.Text;
            }

            StringBuilder dec = new StringBuilder();
            if (result.Text.StartsWith(",")) {
                result.Text = dec.Insert(0, "0,").ToString();
            }
        }

        /// <summary>
        /// click event for operators and sqrt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void operator_Click(object sender, EventArgs e) {
            Button buttonPressed = (Button)sender;

            if(value != 0){
                if (buttonPressed.Text == "sqrt") {
                    result.Text = Math.Sqrt(double.Parse(result.Text)).ToString();
                }
                equals.PerformClick();
                operationPress = true;
                operation = buttonPressed.Text;
            }
            else if (buttonPressed.Text == "sqrt") {
                result.Text = Math.Sqrt(double.Parse(result.Text)).ToString();
                value = Math.Sqrt(double.Parse(result.Text));
            }

            operation = buttonPressed.Text;
            value = double.Parse(result.Text);
            operationPress = true;
            resultEquationBox.Text = value + " " + operation;
        }

        /// <summary>
        /// les opérations arithmétiques et l'affichage du résultat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void equationControl_Click(object sender, EventArgs e) {
            switch (operation){
                case "+": result.Text = (value + double.Parse(result.Text)).ToString();
                    break;
                case "-": result.Text = (value - double.Parse(result.Text)).ToString();
                    break;
                case "/": result.Text = (value / double.Parse(result.Text)).ToString();
                    break;
                case "*": result.Text = (value * double.Parse(result.Text)).ToString();
                    break;
                default:
                    break;
            }
            value = double.Parse(result.Text);           
            operation = "";
            equalPressed = true;
            resultEquationBox.Text = value + " " + operation;
        }

        /// <summary>
        /// efface le dernier chiffre entré
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backspace_Click(object sender, EventArgs e) {          
    	    string str;
    	    int loc;
    	    if (result.Text.Length > 0) {
                str = result.Text.Substring(result.Text.Length - 1);
                loc = result.Text.Length;
                result.Text = result.Text.Remove(loc - 1, 1);
           }
        }

        /// <summary>
        /// mettre '-' s'il n'est pas déja présent ou l'enlever 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void negatif_Click(object sender, EventArgs e) {  
            if (result.Text.StartsWith("-")) {
                result.Text = result.Text.Substring(1);
            }
            else if (!string.IsNullOrEmpty(result.Text)) {
                    result.Text = "-" + result.Text;
            }          
        }

        /// <summary>
        /// test pour faire fonctionner la calc avec le clavier en simulant un "button click"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCalculator_KeyPress(object sender, KeyPressEventArgs e) {
            switch(e.KeyChar.ToString()){
                case "0": num0.PerformClick();
                    break;
                case "1": num1.PerformClick();
                    break;
                case "2": num2.PerformClick();
                    break;
                case "3": num3.PerformClick();
                    break;
                case "4": num4.PerformClick();
                    break;
                case "5": num5.PerformClick();
                    break;
                case "6": num6.PerformClick();
                    break;
                case "7": num7.PerformClick();
                    break;
                case "8": num8.PerformClick();
                    break;
                case "9": num9.PerformClick();
                    break;
                case "+": addition.PerformClick();
                    break;
                case "-": soustraction.PerformClick();
                    break;
                case "*": multiplication.PerformClick();
                    break;
                case "/": division.PerformClick();
                    break;
                case ",": points.PerformClick();
                    break;
                case "=": equals.PerformClick();
                    break;
            }
        }

        /// <summary>
        /// vide la mémoire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clear_Click(object sender, EventArgs e) {
            result.Text = "0";
            value = 0;
            resultEquationBox.Text = "";
        }

        /// <summary>
        /// calcul le pourcentage a partir de la valeur affichée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void percentage_Click(object sender, EventArgs e) {
            decimal currVal = decimal.Parse(result.Text);
            currVal = currVal / 100;
            result.Text = currVal.ToString();
            resultEquationBox.Text = currVal + " ";
        }

        /// <summary>
        /// calcul de la function inverse du nombre affiché
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inverse_Click(object sender, EventArgs e) {
            decimal currVal = decimal.Parse(result.Text);
            if (currVal != 0m) {          
                currVal = 1 / currVal;
                result.Text = currVal.ToString();               
            }
            resultEquationBox.Text = currVal + " ";
        }
    }
}