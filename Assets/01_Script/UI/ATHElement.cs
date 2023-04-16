/*   
    GameJam template for unity project
    Copyright (C) 2023  VladimirChantitch-MarmotteQuantique

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using savesystem.realm;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using System.ComponentModel;

namespace ui.template
{
    public class ATHElement : AbstractTemplateElement
    {
        public new class UxmlFactory : UxmlFactory<ATHElement, ATHElement.UxmlTraits> { }

        public ATHElement() { }

        public Label l_score = null;
        public ProgressBar progressBar = null;

        public void Init(PlayerRealm playerRealm)
        {
            BindUI(playerRealm);
        }

        private void BindUI(PlayerRealm playerRealm)
        {
            l_score = this.Q<Label>("l_score");
            progressBar = this.Q<ProgressBar>();

            playerRealm.PropertyChanged += (obj, args) =>
            {
                l_score.text = (obj as PlayerRealm).highScore.ToString();
            };
        }

    }
}
