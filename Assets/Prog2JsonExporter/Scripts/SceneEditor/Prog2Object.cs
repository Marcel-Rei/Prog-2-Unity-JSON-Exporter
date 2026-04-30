using System.Collections.Generic;
using System.IO;
using System.Linq;
using Prog2JsonExporter.Scripts.Data;
using Prog2JsonExporter.Scripts.Example;
using Prog2JsonExporter.Scripts.Settings;
using UnityEditor;
using UnityEngine;

namespace Prog2JsonExporter.Scripts.SceneEditor
{
    public class Prog2Object : MonoBehaviour
    {
        private Prog2ObjectData _prog2ObjectData;
        public Prog2ObjectData GetLevelObjectData(Prog2JsonExportSettingsContext settingsContext)
        {
            _prog2ObjectData = new Prog2ObjectData
            {
                xPosition = transform.position.x,
                yPosition = transform.position.y
            };
            
            if (GetComponent<Prog2EntityComponent>() == null)
            {
                LoadSpriteData(settingsContext);
                LoadColliderData(settingsContext);
            }
            LoadCustomData();
            
            return _prog2ObjectData;
        }

        private void LoadCustomData()
        {
            Prog2CustomObjectComponent[] customDataComponents = GetComponents<Prog2CustomObjectComponent>();
            
            Prog2CustomData[] customData = customDataComponents.Select(c => c.GetCustomData()).ToArray();
            
            if (customData.Length == 0)
            {
                _prog2ObjectData.customObjectData = null;
            }
            else
            {
                _prog2ObjectData.customObjectData = customData;
            }
        }
        
        private void LoadColliderData(Prog2JsonExportSettingsContext settingsContext)
        {
            _prog2ObjectData.xPosition = transform.position.x;
            _prog2ObjectData.yPosition = transform.position.y;

            Collider2D col = GetComponent<Collider2D>();
            if (col != null)
            {
                if (col is PolygonCollider2D poly)
                {
                    Prog2Polygon polygon = new Prog2Polygon();
                    polygon.points = new List<Prog2Vector2>();

                    foreach (Vector2 point in poly.points)
                    {
                        Prog2Vector2 p = new Prog2Vector2();

                        if (settingsContext.ShouldRoundDownColliders)
                        {
                            p.x = Mathf.FloorToInt(point.x);
                            p.y = Mathf.FloorToInt(point.y);
                        }

                        polygon.points.Add(p);
                    }

                    _prog2ObjectData.prog2Polygon = polygon;
                    _prog2ObjectData.prog2Rectf = null;
                }
                else
                {
                    Vector2 localBottomLeft = col.offset - (Vector2)((col.bounds.size / 2f));

                    Prog2Rectf rectf = new Prog2Rectf();

                    if (settingsContext.ShouldRoundDownColliders)
                    {
                        rectf.left = Mathf.FloorToInt(localBottomLeft.x);
                        rectf.bottom = Mathf.FloorToInt(localBottomLeft.y);
                        rectf.width = Mathf.FloorToInt(col.bounds.size.x);
                        rectf.height = Mathf.FloorToInt(col.bounds.size.y);
                    }
                    else
                    {
                        rectf.left = localBottomLeft.x;
                        rectf.bottom = localBottomLeft.y;
                        rectf.width = col.bounds.size.x;
                        rectf.height = col.bounds.size.y;
                    }

                    if (settingsContext.ShouldExportIsTriggerInfo)
                    {
                        _prog2ObjectData.isTrigger = col.isTrigger;
                    }
                    else
                    {
                        _prog2ObjectData.isTrigger = null;
                    }

                    _prog2ObjectData.prog2Rectf = rectf;
                }
            }
            else
            {
                _prog2ObjectData.prog2Rectf = null;
            }
        }
        
        private void LoadSpriteData(Prog2JsonExportSettingsContext settingsContext)
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();

            if (sprite)
            {
                _prog2ObjectData.texturePath = GetPath(sprite.sprite);
                _prog2ObjectData.scale = new Prog2Vector2();
                _prog2ObjectData.scale.x = transform.localScale.x;
                _prog2ObjectData.scale.y = transform.localScale.y;
                _prog2ObjectData.isFlipped = sprite.flipX;
                
                if (settingsContext.ShouldExportRenderLayer)
                {
                    _prog2ObjectData.renderLayer = sprite.sortingOrder;
                }
                else
                {
                    _prog2ObjectData.renderLayer = null;
                }
                
            }
            else
            {
                _prog2ObjectData.texturePath = null;
            }
        }
        
        private string GetPath(Sprite sprite)
        {
            string fullPath = AssetDatabase.GetAssetPath(sprite);
            string fileName = Path.GetFileName(fullPath);
            return fileName;
        }
    }
}